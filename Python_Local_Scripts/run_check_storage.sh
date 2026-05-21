#!/bin/bash
#
# Diff entre Question images esperadas (JSON exports do DatabaseExporter) e o
# conteúdo real do Firebase Storage. Não modifica nada.
#
# Usage:
#   ./run_check_storage.sh                    # imprime relatório no terminal
#   ./run_check_storage.sh report.txt         # também salva em arquivo

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_SCRIPT="$SCRIPT_DIR/check_storage_images.py"
JSON_DIR="/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings"
BUCKET="gs://microlearning-dev-79c0c.firebasestorage.app"

if [ ! -d "$JSON_DIR" ]; then
    echo "❌ JSON directory not found: $JSON_DIR"
    exit 1
fi

if [ ! -f "$PYTHON_SCRIPT" ]; then
    echo "❌ Python script not found: $PYTHON_SCRIPT"
    exit 1
fi

echo "Checking dependencies..."
python3 -c "import firebase_admin" 2>/dev/null || {
    echo "Installing firebase-admin..."
    pip install firebase-admin
}

CMD="python3 \"$PYTHON_SCRIPT\""
CMD="$CMD --json-dir \"$JSON_DIR\""
CMD="$CMD --bucket \"$BUCKET\""
CMD="$CMD --service-account \"$HOME/firebase-key.json\""

if [ -n "$1" ]; then
    CMD="$CMD --write-report \"$1\""
fi

eval "$CMD"
