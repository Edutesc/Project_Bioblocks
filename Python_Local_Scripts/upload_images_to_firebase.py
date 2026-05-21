#!/usr/bin/env python3
"""
Upload Local Images to Firebase Storage

Lê os JSON files exportados e faz upload das imagens locais para Firebase Storage
organizadas na estrutura Question/<topic>/, onde <topic> corresponde ao tema
(ex.: acidsBase, biochem, water, ...). Cada tema agrupa todas as imagens
(de pergunta e de resposta) das suas questões.

Usa os paths dos JSONs para encontrar as imagens localmente e reorganiza tudo ao fazer upload.

Usage:
    python3 upload_images_to_firebase.py \
        --json-dir "/path/to/ImageMappings" \
        --resources-dir "/path/to/Assets/Resources" \
        --bucket "gs://your-bucket-name" \
        --service-account "/path/to/serviceAccountKey.json" \
        [--dry-run]
"""

import argparse
import json
import os
import sys
from pathlib import Path
from typing import Optional, Tuple, Dict, List
import logging

try:
    import firebase_admin
    from firebase_admin import credentials, storage
except ImportError:
    print("Error: firebase-admin not installed. Install with:")
    print("  pip install firebase-admin")
    sys.exit(1)


# ─────────────────────────────────────────────────────────────────────────────
# Configuration
# ─────────────────────────────────────────────────────────────────────────────

logging.basicConfig(
    level=logging.INFO,
    format='[%(levelname)s] %(message)s'
)
logger = logging.getLogger(__name__)


# Mapeamento das classes de databank (nome longo, conforme exportado no JSON)
# para o código de tema (topic) usado como pasta no Firebase Storage.
# Os topics seguem o enum QuestionSystem.QuestionSet.
DATABANK_TO_TOPIC: Dict[str, str] = {
    "AcidBaseBufferQuestionDatabase":        "acidsBase",
    "AminoacidQuestionDatabase":             "aminoacids",
    "BiochemistryIntroductionQuestionDatabase": "biochem",
    "CarbohydratesQuestionDatabase":         "carbohydrates",
    "EnzymeQuestionDatabase":                "enzymes",
    "LipidsQuestionDatabase":                "lipids",
    "MembranesQuestionDatabase":             "membranes",
    "NucleicAcidsQuestionDatabase":          "nucleicAcids",
    "ProteinQuestionDatabase":               "proteins",
    "WaterQuestionDatabase":                 "water",
}


def resolve_topic(databank_name: str) -> Optional[str]:
    """Resolve o code de tema (topic) a partir do nome da databank."""
    if not databank_name:
        return None
    return DATABANK_TO_TOPIC.get(databank_name)


# ─────────────────────────────────────────────────────────────────────────────
# Core Logic
# ─────────────────────────────────────────────────────────────────────────────

def extract_bucket_name(bucket_url: str) -> str:
    """Extract bucket name from gs:// URL or return as-is."""
    if bucket_url.startswith("gs://"):
        return bucket_url[5:]
    return bucket_url


def find_image_locally(resources_dir: Path, relative_path: str) -> Optional[Path]:
    """
    Find an image file locally in Assets/Resources/.

    Tries multiple strategies:
    1. Direct path relative to resources_dir
    2. Search by filename if path is just filename
    3. Recursive search in resources_dir
    """
    # Strategy 1: Direct path
    full_path = resources_dir / relative_path
    if full_path.exists() and full_path.is_file():
        return full_path

    # Strategy 2: If relative_path is just a filename (no slashes), search for it
    if '/' not in relative_path:
        filename = relative_path
        for file in resources_dir.rglob(filename):
            if file.is_file():
                return file

    # Strategy 3: Try with common image extensions if none provided
    if not relative_path.lower().endswith(('.png', '.jpg', '.jpeg', '.gif', '.webp')):
        for ext in ['.png', '.jpg', '.jpeg', '.gif', '.webp']:
            full_path_with_ext = resources_dir / (relative_path + ext)
            if full_path_with_ext.exists() and full_path_with_ext.is_file():
                return full_path_with_ext

    return None


def load_json_mapping(json_dir: Path, resources_dir: Path) -> Tuple[Dict, Dict]:
    """
    Load JSON files and build mapping of upload targets to local source files.

    A SAME local image can be referenced by questions in multiple topics
    (e.g. an aminoacid image used as answer in a biochem question). In that
    case it must be uploaded to BOTH `Question/<topic>/<filename>` paths.
    The dict key is the target — guarantees each target is uploaded exactly
    once, while the same source can map to several targets.

    Returns:
        Tuple of (upload_mapping, missing_files)
        upload_mapping: {target_firebase_path: local_path}
        missing_files: {relative_path: reason}
    """
    upload_mapping = {}
    missing_files = {}

    json_files = sorted(json_dir.glob("*.json"))
    if not json_files:
        logger.warning(f"No JSON files found in {json_dir}")
        return upload_mapping, missing_files

    logger.info(f"Found {len(json_files)} JSON file(s)")

    for json_file in json_files:
        try:
            with open(json_file, 'r', encoding='utf-8') as f:
                data = json.load(f)

            databank_name = data.get('databankName', 'unknown')
            questions = data.get('questions', [])

            topic = resolve_topic(databank_name)
            if not topic:
                logger.error(
                    f"Topic não mapeado para databank '{databank_name}' "
                    f"(arquivo {json_file.name}). Atualize DATABANK_TO_TOPIC."
                )
                continue

            # Layout no Storage: Question/<topic>/<filename>
            # Ex.: Question/biochem/benzeno.png
            target_folder = f"Question/{topic}/"

            logger.info(
                f"Processing {json_file.name}: {len(questions)} questions "
                f"(topic='{topic}')"
            )

            for question in questions:
                question_number = question.get('questionNumber')
                question_image_path = question.get('questionImagePath')
                answer_images = question.get('answerImages', [])

                if question_number is None:
                    continue

                # Process question image
                if question_image_path and question_image_path.strip():
                    relative_path = question_image_path.strip()
                    local_file = find_image_locally(resources_dir, relative_path)

                    if local_file:
                        filename = local_file.name
                        target_path = f"{target_folder}{filename}"
                        # Dict key = target → cada destino é único; o mesmo arquivo
                        # local pode aparecer como source em vários topics.
                        upload_mapping[target_path] = str(local_file)
                        logger.debug(f"  [{topic}] Q{question_number}: {local_file.name} → {target_path}")
                    else:
                        missing_files[relative_path] = f"[{topic}] Q{question_number} question image"

                # Process answer images
                for answer_image_path in answer_images:
                    if answer_image_path and answer_image_path.strip():
                        relative_path = answer_image_path.strip()
                        local_file = find_image_locally(resources_dir, relative_path)

                        if local_file:
                            filename = local_file.name
                            target_path = f"{target_folder}{filename}"
                            upload_mapping[target_path] = str(local_file)
                            logger.debug(f"  [{topic}] Q{question_number}: {local_file.name} → {target_path}")
                        else:
                            missing_files[relative_path] = f"[{topic}] Q{question_number} answer image"

        except json.JSONDecodeError as e:
            logger.error(f"Error reading {json_file.name}: {e}")
        except Exception as e:
            logger.error(f"Unexpected error processing {json_file.name}: {e}")

    logger.info(f"Mapping complete: {len(upload_mapping)} images ready to upload")
    if missing_files:
        logger.warning(f"⚠️  {len(missing_files)} images not found locally")

    return upload_mapping, missing_files


def initialize_firebase(service_account_path: Optional[str], bucket_name: str):
    """Initialize Firebase Admin SDK and return bucket reference."""
    if service_account_path:
        if not os.path.exists(service_account_path):
            raise FileNotFoundError(f"Service account file not found: {service_account_path}")

        logger.info(f"Loading credentials from: {service_account_path}")
        cred = credentials.Certificate(service_account_path)
        firebase_admin.initialize_app(cred, {'storageBucket': bucket_name})
        return storage.bucket()

    elif os.getenv('GOOGLE_APPLICATION_CREDENTIALS'):
        cred_path = os.getenv('GOOGLE_APPLICATION_CREDENTIALS')
        logger.info(f"Loading credentials from environment: {cred_path}")
        cred = credentials.Certificate(cred_path)
        firebase_admin.initialize_app(cred, {'storageBucket': bucket_name})
        return storage.bucket()

    else:
        logger.info("Using default application credentials")
        try:
            firebase_admin.initialize_app({'storageBucket': bucket_name})
            return storage.bucket()
        except Exception as e:
            raise RuntimeError(
                f"Could not initialize Firebase:\n"
                f"  {e}\n"
                f"\n"
                f"Provide credentials via:\n"
                f"  1. --service-account flag with path to serviceAccountKey.json\n"
                f"  2. GOOGLE_APPLICATION_CREDENTIALS environment variable"
            )


def upload_image(bucket, local_file_path: str, firebase_path: str, dry_run: bool = True) -> bool:
    """Upload a single image to Firebase Storage. Sobrescreve se já existir."""
    try:
        if dry_run:
            file_size = Path(local_file_path).stat().st_size
            size_mb = file_size / (1024 * 1024)
            logger.info(f"  [DRY RUN] Would upload: {Path(local_file_path).name} ({size_mb:.2f} MB) → {firebase_path}")
            return True

        file_size = Path(local_file_path).stat().st_size
        size_mb = file_size / (1024 * 1024)

        blob = bucket.blob(firebase_path)
        blob.upload_from_filename(local_file_path)

        logger.info(f"  ✓ Uploaded: {Path(local_file_path).name} ({size_mb:.2f} MB) → {firebase_path}")
        return True

    except Exception as e:
        logger.error(f"  ✗ Error uploading {local_file_path}: {e}")
        return False


def upload_all_images(bucket, upload_mapping: Dict, dry_run: bool = True) -> Dict:
    """Upload all images to Firebase Storage. Sempre sobrescreve."""
    stats = {
        'total':   len(upload_mapping),
        'success': 0,
        'failed':  0,
    }

    if dry_run:
        logger.info("=" * 80)
        logger.info("DRY RUN MODE - No uploads will be made")
        logger.info("=" * 80)
    else:
        logger.info("=" * 80)
        logger.info("UPLOAD MODE — Uploading to Firebase Storage (overwrite)")
        logger.info("=" * 80)

    for i, (firebase_path, local_path) in enumerate(upload_mapping.items(), 1):
        logger.info(f"[{i}/{len(upload_mapping)}] Processing...")

        if upload_image(bucket, local_path, firebase_path, dry_run):
            stats['success'] += 1
        else:
            stats['failed'] += 1

    return stats


def print_summary(stats: Dict, missing_count: int = 0, dry_run: bool = True):
    """Print final summary."""
    logger.info("")
    logger.info("=" * 80)
    logger.info("SUMMARY")
    logger.info("=" * 80)
    logger.info(f"Total images to upload:    {stats['total']}")
    logger.info(f"Successfully uploaded:     {stats['success']}")
    logger.info(f"Failed:                    {stats['failed']}")
    logger.info(f"Not found locally:         {missing_count}")

    if dry_run:
        logger.info("")
        logger.info("This was a DRY RUN. To apply changes, run without --dry-run flag.")

    logger.info("=" * 80)


def print_missing_files(missing_files: Dict):
    """Print list of missing files."""
    if not missing_files:
        return

    logger.warning("")
    logger.warning("=" * 80)
    logger.warning("MISSING FILES (not found locally)")
    logger.warning("=" * 80)

    for relative_path, context in sorted(missing_files.items()):
        logger.warning(f"  {relative_path} ({context})")

    logger.warning("=" * 80)


# ─────────────────────────────────────────────────────────────────────────────
# CLI
# ─────────────────────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(
        description='Upload local images to Firebase Storage in organized structure'
    )

    parser.add_argument(
        '--json-dir',
        type=Path,
        required=True,
        help='Directory containing exported JSON files from DatabaseExporter'
    )

    parser.add_argument(
        '--resources-dir',
        type=Path,
        required=True,
        help='Path to Assets/Resources folder containing the images'
    )

    parser.add_argument(
        '--bucket',
        type=str,
        required=True,
        help='Firebase Storage bucket (gs://bucket-name or bucket-name)'
    )

    parser.add_argument(
        '--service-account',
        type=str,
        default=None,
        help='Path to Firebase service account JSON key'
    )

    parser.add_argument(
        '--apply',
        action='store_true',
        help='Sobe os arquivos pro Firebase Storage. Sem essa flag, roda em '
             'dry-run: lista o que seria enviado mas não faz upload.'
    )

    parser.add_argument(
        '--verbose',
        action='store_true',
        help='Enable verbose logging'
    )

    args = parser.parse_args()

    # Configure logging
    if args.verbose:
        logging.getLogger().setLevel(logging.DEBUG)

    # Validate arguments
    if not args.json_dir.exists():
        logger.error(f"JSON directory not found: {args.json_dir}")
        sys.exit(1)

    if not args.resources_dir.exists():
        logger.error(f"Resources directory not found: {args.resources_dir}")
        sys.exit(1)

    dry_run = not args.apply
    bucket_name = extract_bucket_name(args.bucket)

    try:
        # Step 1: Load JSON and find local files
        logger.info("")
        logger.info("Step 1: Loading JSON mappings and finding local images")
        logger.info("-" * 80)
        upload_mapping, missing_files = load_json_mapping(args.json_dir, args.resources_dir)

        if not upload_mapping:
            logger.error("No images found to upload.")
            sys.exit(1)

        # Step 2: Initialize Firebase
        logger.info("")
        logger.info("Step 2: Initializing Firebase Admin SDK")
        logger.info("-" * 80)
        bucket = initialize_firebase(args.service_account, bucket_name)
        logger.info(f"Connected to bucket: {bucket_name}")

        # Step 3: Upload images
        logger.info("")
        logger.info("Step 3: Uploading images")
        logger.info("-" * 80)
        stats = upload_all_images(bucket, upload_mapping, dry_run)

        # Step 4: Print summary
        logger.info("")
        print_summary(stats, len(missing_files), dry_run)

        if missing_files:
            print_missing_files(missing_files)

        # Exit codes
        if stats['failed'] > 0:
            logger.warning("Some images failed to upload.")
            sys.exit(1)

        sys.exit(0)

    except KeyboardInterrupt:
        logger.warning("Interrupted by user")
        sys.exit(130)

    except Exception as e:
        logger.error(f"Fatal error: {e}")
        if args.verbose:
            import traceback
            traceback.print_exc()
        sys.exit(1)


if __name__ == '__main__':
    main()
