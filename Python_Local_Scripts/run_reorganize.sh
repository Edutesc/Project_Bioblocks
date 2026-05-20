#!/bin/bash
#
# Helper script to run Firebase Storage reorganization
#
# Usage:
#   ./run_reorganize.sh [--apply]
#
# First run (dry-run to preview):
#   ./run_reorganize.sh
#
# Second run (apply changes):
#   ./run_reorganize.sh --apply

set -e

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_SCRIPT="$SCRIPT_DIR/reorganize_firebase_storage.py"
JSON_DIR="/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings"
BUCKET="gs://microlearning-dev-79c0c.appspot.com"

# Check if JSON directory exists
if [ ! -d "$JSON_DIR" ]; then
    echo "❌ JSON directory not found: $JSON_DIR"
    exit 1
fi

# Check if Python script exists
if [ ! -f "$PYTHON_SCRIPT" ]; then
    echo "❌ Python script not found: $PYTHON_SCRIPT"
    exit 1
fi

# Ensure firebase-admin is installed
echo "Checking dependencies..."
python3 -c "import firebase_admin" 2>/dev/null || {
    echo "Installing firebase-admin..."
    pip install firebase-admin
}

# Build command
CMD="python3 $PYTHON_SCRIPT"
CMD="$CMD --json-dir \"$JSON_DIR\""
CMD="$CMD --bucket \"$BUCKET\""
CMD="$CMD --verbose"

# Check for --apply flag
if [ "$1" = "--apply" ]; then
    echo ""
    echo "⚠️  APPLYING CHANGES TO FIREBASE STORAGE"
    echo ""
    sleep 2
    CMD="$CMD --apply"
else
    CMD="$CMD --dry-run"
fi

# Run
eval "$CMD"
