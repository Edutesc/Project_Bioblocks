#!/bin/bash
#
# Upload local images to Firebase Storage
#
# Usage:
#   ./run_upload.sh [--apply]
#
# First run (dry-run to preview):
#   ./run_upload.sh
#
# Second run (apply):
#   ./run_upload.sh --apply

set -e

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_SCRIPT="$SCRIPT_DIR/upload_images_to_firebase.py"
JSON_DIR="/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings"
RESOURCES_DIR="$SCRIPT_DIR/Assets/Resources"
BUCKET="gs://microlearning-dev-79c0c.firebasestorage.app"

# Check if directories exist
if [ ! -d "$JSON_DIR" ]; then
    echo "❌ JSON directory not found: $JSON_DIR"
    exit 1
fi

if [ ! -d "$RESOURCES_DIR" ]; then
    echo "❌ Resources directory not found: $RESOURCES_DIR"
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
CMD="$CMD --resources-dir \"$RESOURCES_DIR\""
CMD="$CMD --bucket \"$BUCKET\""
CMD="$CMD --service-account \"$HOME/firebase-key.json\""
CMD="$CMD --verbose"

# Check for --apply flag
if [ "$1" = "--apply" ]; then
    echo ""
    echo "⚠️  UPLOADING IMAGES TO FIREBASE STORAGE"
    echo "This will upload ~164 images (~15MB)"
    echo ""
    sleep 2
    CMD="$CMD --apply"
else
    CMD="$CMD --dry-run"
fi

# Run
eval "$CMD"
