#!/usr/bin/env python3
"""
Check Storage Images — Diff entre o que o app espera e o que está no bucket.

Usa os JSON files exportados pelo DatabaseExporter (mesma fonte que o
upload_images_to_firebase.py) para listar TODAS as imagens que o app vai
tentar baixar. Depois lista o conteúdo do Firebase Storage e mostra:

  • MISSING_FROM_STORAGE  → app vai pedir, bucket não tem (subir ou tirar
                              referência do Firestore)
  • EXTRA_IN_STORAGE       → bucket tem, app não pede (candidato a limpeza)
  • Total esperado vs total real

Não modifica nada — só relata.

Usage:
    python3 check_storage_images.py \
        --json-dir "/path/to/ImageMappings" \
        --bucket "gs://your-bucket-name" \
        --service-account "/path/to/serviceAccountKey.json" \
        [--write-report report.txt]
"""

import argparse
import json
import logging
import os
import sys
from pathlib import Path
from typing import Dict, List, Optional, Set, Tuple

try:
    import firebase_admin
    from firebase_admin import credentials, storage
except ImportError:
    print("Error: firebase-admin not installed. Install with:")
    print("  pip install firebase-admin")
    sys.exit(1)


logging.basicConfig(level=logging.INFO, format='[%(levelname)s] %(message)s')
logger = logging.getLogger(__name__)


# Mesmo mapeamento de upload_images_to_firebase.py — manter sincronizado.
DATABANK_TO_TOPIC: Dict[str, str] = {
    "AcidBaseBufferQuestionDatabase":           "acidsBase",
    "AminoacidQuestionDatabase":                "aminoacids",
    "BiochemistryIntroductionQuestionDatabase": "biochem",
    "CarbohydratesQuestionDatabase":            "carbohydrates",
    "EnzymeQuestionDatabase":                   "enzymes",
    "LipidsQuestionDatabase":                   "lipids",
    "MembranesQuestionDatabase":                "membranes",
    "NucleicAcidsQuestionDatabase":             "nucleicAcids",
    "ProteinQuestionDatabase":                  "proteins",
    "WaterQuestionDatabase":                    "water",
}


def extract_filename(path: str) -> Optional[str]:
    """Extrai só o filename de um path legado tipo 'AnswerImages/Foo/bar' ou 'bar'."""
    if not path:
        return None
    name = path.strip().split('/')[-1]
    if not name:
        return None
    # Tira extensão se vier — o storage está em .png
    base = os.path.splitext(name)[0]
    return base if base else None


def collect_expected(json_dir: Path) -> Set[Tuple[str, str]]:
    """
    Lê os JSONs e devolve o conjunto de (topic, filename) que o app precisa.
    Considera tanto questionImagePath quanto answerImages.
    """
    expected: Set[Tuple[str, str]] = set()

    json_files = sorted(json_dir.glob("*.json"))
    if not json_files:
        logger.warning(f"Nenhum JSON em {json_dir}")
        return expected

    for json_file in json_files:
        try:
            with open(json_file, 'r', encoding='utf-8') as f:
                data = json.load(f)

            databank = data.get('databankName', '')
            topic = DATABANK_TO_TOPIC.get(databank)
            if not topic:
                logger.warning(f"Topic não mapeado para '{databank}' em {json_file.name}")
                continue

            questions = data.get('questions', [])
            for q in questions:
                qip = q.get('questionImagePath')
                if qip:
                    fn = extract_filename(qip)
                    if fn: expected.add((topic, fn))

                for ai in (q.get('answerImages') or []):
                    fn = extract_filename(ai)
                    if fn: expected.add((topic, fn))

        except json.JSONDecodeError as e:
            logger.error(f"JSON inválido em {json_file.name}: {e}")
        except Exception as e:
            logger.error(f"Erro lendo {json_file.name}: {e}")

    return expected


def collect_actual(bucket, prefix: str = "Question/") -> Set[Tuple[str, str]]:
    """
    Lista todos os blobs no bucket sob o prefixo Question/ e retorna
    o conjunto (topic, filename_sem_extensao).
    """
    actual: Set[Tuple[str, str]] = set()

    for blob in bucket.list_blobs(prefix=prefix):
        # Esperado: Question/<topic>/<filename>.png
        rel = blob.name[len(prefix):] if blob.name.startswith(prefix) else blob.name
        if '/' not in rel:
            continue
        topic, filename = rel.split('/', 1)
        if not filename:
            continue
        base = os.path.splitext(filename)[0]
        actual.add((topic, base))

    return actual


def initialize_firebase(service_account_path: Optional[str], bucket_name: str):
    if service_account_path:
        if not os.path.exists(service_account_path):
            raise FileNotFoundError(f"Service account não encontrado: {service_account_path}")
        cred = credentials.Certificate(service_account_path)
    elif os.getenv('GOOGLE_APPLICATION_CREDENTIALS'):
        cred = credentials.Certificate(os.getenv('GOOGLE_APPLICATION_CREDENTIALS'))
    else:
        firebase_admin.initialize_app({'storageBucket': bucket_name})
        return storage.bucket()

    firebase_admin.initialize_app(cred, {'storageBucket': bucket_name})
    return storage.bucket()


def extract_bucket_name(bucket_url: str) -> str:
    return bucket_url[5:] if bucket_url.startswith("gs://") else bucket_url


def format_pair(topic: str, filename: str) -> str:
    return f"{topic}/{filename}"


def print_report(expected: Set[Tuple[str, str]],
                 actual:   Set[Tuple[str, str]],
                 missing:  Set[Tuple[str, str]],
                 extra:    Set[Tuple[str, str]],
                 out_file: Optional[Path]):
    lines = []
    lines.append("=" * 80)
    lines.append("STORAGE IMAGES DIFF")
    lines.append("=" * 80)
    lines.append(f"Esperado pelas Question (JSON):  {len(expected)}")
    lines.append(f"Encontrado no Storage:            {len(actual)}")
    lines.append(f"Faltando no Storage:              {len(missing)}")
    lines.append(f"Extras no Storage:                {len(extra)}")
    lines.append("")

    if missing:
        lines.append("─" * 80)
        lines.append("MISSING_FROM_STORAGE — app vai pedir, bucket não tem")
        lines.append("─" * 80)
        # Agrupa por topic pra ficar legível
        by_topic: Dict[str, List[str]] = {}
        for topic, fn in sorted(missing):
            by_topic.setdefault(topic, []).append(fn)
        for topic in sorted(by_topic.keys()):
            lines.append(f"\n  {topic}/  ({len(by_topic[topic])} imagens):")
            for fn in by_topic[topic]:
                lines.append(f"    - {fn}.png")
        lines.append("")

    if extra:
        lines.append("─" * 80)
        lines.append("EXTRA_IN_STORAGE — bucket tem, app não pede (candidatos a limpeza)")
        lines.append("─" * 80)
        by_topic = {}
        for topic, fn in sorted(extra):
            by_topic.setdefault(topic, []).append(fn)
        for topic in sorted(by_topic.keys()):
            lines.append(f"\n  {topic}/  ({len(by_topic[topic])} imagens):")
            for fn in by_topic[topic]:
                lines.append(f"    - {fn}.png")
        lines.append("")

    lines.append("=" * 80)

    text = "\n".join(lines)
    print(text)

    if out_file:
        out_file.write_text(text, encoding='utf-8')
        logger.info(f"Relatório salvo em {out_file}")


def main():
    parser = argparse.ArgumentParser(description='Diff entre Question images esperadas e o conteúdo do Firebase Storage')
    parser.add_argument('--json-dir',         type=Path, required=True,
                        help='Diretório com os JSONs do DatabaseExporter')
    parser.add_argument('--bucket',           type=str,  required=True,
                        help='Bucket (gs://… ou só o nome)')
    parser.add_argument('--service-account',  type=str,  default=None,
                        help='Service account JSON (opcional)')
    parser.add_argument('--write-report',     type=Path, default=None,
                        help='Caminho de saída para salvar o relatório em arquivo')
    parser.add_argument('--prefix',           type=str,  default='Question/',
                        help='Prefixo no bucket (default: Question/)')

    args = parser.parse_args()

    if not args.json_dir.exists():
        logger.error(f"JSON dir não existe: {args.json_dir}")
        sys.exit(1)

    bucket_name = extract_bucket_name(args.bucket)

    logger.info("Coletando imagens esperadas a partir dos JSONs...")
    expected = collect_expected(args.json_dir)
    logger.info(f"  → {len(expected)} (topic, filename) esperados.")

    logger.info("Conectando ao Firebase Storage...")
    bucket = initialize_firebase(args.service_account, bucket_name)
    logger.info(f"  → bucket {bucket_name}")

    logger.info(f"Listando conteúdo de {args.prefix}...")
    actual = collect_actual(bucket, args.prefix)
    logger.info(f"  → {len(actual)} blobs encontrados.")

    missing = expected - actual
    extra   = actual - expected

    print_report(expected, actual, missing, extra, args.write_report)

    sys.exit(0 if not missing else 2)  # exit 2 se houver faltantes


if __name__ == '__main__':
    main()
