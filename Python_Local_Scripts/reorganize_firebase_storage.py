#!/usr/bin/env python3
"""
Firebase Storage Image Reorganizer

Reads exported JSON files from DatabaseExporter and reorganizes Firebase Storage
from scattered image paths into a clean Question/Question{N}/ structure.

Usage:
    python3 reorganize_firebase_storage.py \
        --json-dir "/path/to/ImageMappings" \
        --bucket "gs://your-bucket-name" \
        --service-account "/path/to/serviceAccountKey.json" \
        [--dry-run]

Environment variables (optional, if not using --service-account):
    GOOGLE_APPLICATION_CREDENTIALS - path to service account JSON
"""

import argparse
import json
import os
import sys
from pathlib import Path
from typing import Optional, Tuple
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


# ─────────────────────────────────────────────────────────────────────────────
# Core Logic
# ─────────────────────────────────────────────────────────────────────────────

def extract_bucket_name(bucket_url: str) -> str:
    """
    Extract bucket name from gs:// URL or return as-is if already a bucket name.

    Examples:
        "gs://microlearning-dev-79c0c.appspot.com" -> "microlearning-dev-79c0c.appspot.com"
        "microlearning-dev-79c0c.appspot.com" -> "microlearning-dev-79c0c.appspot.com"
    """
    if bucket_url.startswith("gs://"):
        return bucket_url[5:]
    return bucket_url


def load_json_mapping(json_dir: Path) -> dict:
    """
    Load all exported JSON files and build a mapping of source paths to target paths.

    Returns:
        {
            "source_path": "target_path_in_new_structure",
            ...
        }
    """
    mapping = {}

    json_files = sorted(json_dir.glob("*.json"))
    if not json_files:
        logger.warning(f"No JSON files found in {json_dir}")
        return mapping

    logger.info(f"Found {len(json_files)} JSON file(s) in {json_dir}")

    for json_file in json_files:
        try:
            with open(json_file, 'r', encoding='utf-8') as f:
                data = json.load(f)

            databank_name = data.get('databankName', 'unknown')
            questions = data.get('questions', [])

            logger.info(f"Processing {json_file.name}: {databank_name} ({len(questions)} questions with images)")

            for question in questions:
                question_number = question.get('questionNumber')
                question_image_path = question.get('questionImagePath')
                answer_images = question.get('answerImages', [])

                if question_number is None:
                    logger.warning(f"  Question in {databank_name} has no questionNumber, skipping")
                    continue

                # Target folder: Question/Question{N}/
                target_folder = f"Question/Question{question_number}/"

                # Map question image
                if question_image_path and question_image_path.strip():
                    source_path = question_image_path.strip()
                    # Preserve original filename
                    filename = source_path.split('/')[-1]
                    target_path = f"{target_folder}{filename}"
                    mapping[source_path] = target_path
                    logger.debug(f"  Q{question_number}: {source_path} -> {target_path}")

                # Map answer images
                for answer_image_path in answer_images:
                    if answer_image_path and answer_image_path.strip():
                        source_path = answer_image_path.strip()
                        # Preserve original filename
                        filename = source_path.split('/')[-1]
                        target_path = f"{target_folder}{filename}"
                        mapping[source_path] = target_path
                        logger.debug(f"  Q{question_number}: {source_path} -> {target_path}")

        except json.JSONDecodeError as e:
            logger.error(f"Error reading {json_file.name}: {e}")
        except Exception as e:
            logger.error(f"Unexpected error processing {json_file.name}: {e}")

    logger.info(f"Mapping complete: {len(mapping)} images to reorganize")
    return mapping


def initialize_firebase(service_account_path: Optional[str], bucket_name: str):
    """
    Initialize Firebase Admin SDK.

    Args:
        service_account_path: Path to service account JSON (optional, uses env var if not provided)
        bucket_name: Firebase Storage bucket name

    Returns:
        Bucket reference
    """
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
        # Try to use default credentials (may work if gcloud CLI is set up)
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
                f"  2. GOOGLE_APPLICATION_CREDENTIALS environment variable\n"
                f"  3. Firebase CLI setup (gcloud auth application-default login)"
            )


def verify_source_images_exist(bucket, mapping: dict, dry_run: bool = True) -> Tuple[dict, dict]:
    """
    Check which source images exist in Firebase Storage.

    Returns:
        Tuple of (found_mapping, missing_paths)
            found_mapping: paths that exist and should be reorganized
            missing_paths: paths that don't exist
    """
    found_mapping = {}
    missing_paths = {}

    logger.info(f"Verifying {len(mapping)} source image paths...")

    for source_path, target_path in mapping.items():
        try:
            blob = bucket.blob(source_path)
            if blob.exists():
                found_mapping[source_path] = target_path
                logger.debug(f"  ✓ Found: {source_path}")
            else:
                missing_paths[source_path] = target_path
                logger.warning(f"  ✗ Not found: {source_path}")
        except Exception as e:
            missing_paths[source_path] = target_path
            logger.warning(f"  ✗ Error checking {source_path}: {e}")

    logger.info(f"Verification complete:")
    logger.info(f"  {len(found_mapping)} images found")
    logger.info(f"  {len(missing_paths)} images not found")

    return found_mapping, missing_paths


def copy_image(bucket, source_path: str, target_path: str, dry_run: bool = True) -> bool:
    """
    Copy an image from source to target path in Firebase Storage.

    Returns:
        True if successful, False otherwise
    """
    try:
        source_blob = bucket.blob(source_path)

        if dry_run:
            logger.info(f"  [DRY RUN] Would copy: {source_path} -> {target_path}")
            return True

        # Download from source
        logger.debug(f"  Downloading {source_path}...")
        image_data = source_blob.download_as_bytes()

        # Upload to target
        target_blob = bucket.blob(target_path)
        target_blob.upload_from_string(image_data)

        logger.info(f"  ✓ Copied: {source_path} -> {target_path}")
        return True

    except Exception as e:
        logger.error(f"  ✗ Error copying {source_path} -> {target_path}: {e}")
        return False


def reorganize_storage(bucket, mapping: dict, dry_run: bool = True) -> dict:
    """
    Copy all images to new locations.

    Returns:
        Statistics dictionary with success/error counts
    """
    stats = {
        'total': len(mapping),
        'success': 0,
        'failed': 0,
        'skipped': 0,
    }

    if dry_run:
        logger.info("=" * 80)
        logger.info("DRY RUN MODE - No changes will be made to Firebase Storage")
        logger.info("=" * 80)
    else:
        logger.info("=" * 80)
        logger.info("LIVE MODE - Making changes to Firebase Storage")
        logger.info("=" * 80)

    for i, (source_path, target_path) in enumerate(mapping.items(), 1):
        logger.info(f"[{i}/{len(mapping)}] Processing...")

        if copy_image(bucket, source_path, target_path, dry_run):
            stats['success'] += 1
        else:
            stats['failed'] += 1

    return stats


def print_summary(stats: dict, missing_count: int = 0, dry_run: bool = True):
    """Print final summary of reorganization."""
    logger.info("")
    logger.info("=" * 80)
    logger.info("SUMMARY")
    logger.info("=" * 80)
    logger.info(f"Total images mapped:    {stats['total']}")
    logger.info(f"Successfully copied:    {stats['success']}")
    logger.info(f"Failed:                 {stats['failed']}")
    logger.info(f"Not found in source:    {missing_count}")

    if dry_run:
        logger.info("")
        logger.info("This was a DRY RUN. To apply changes, run without --dry-run flag.")

    logger.info("=" * 80)


# ─────────────────────────────────────────────────────────────────────────────
# CLI
# ─────────────────────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(
        description='Reorganize Firebase Storage images using exported JSON metadata'
    )

    parser.add_argument(
        '--json-dir',
        type=Path,
        required=True,
        help='Directory containing exported JSON files from DatabaseExporter'
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
        help='Path to Firebase service account JSON key (optional if GOOGLE_APPLICATION_CREDENTIALS is set)'
    )

    parser.add_argument(
        '--dry-run',
        action='store_true',
        default=True,
        help='Preview changes without applying them (default: True)'
    )

    parser.add_argument(
        '--apply',
        action='store_true',
        help='Actually apply changes to Firebase Storage (must be explicit)'
    )

    parser.add_argument(
        '--verbose',
        action='store_true',
        help='Enable verbose logging (debug level)'
    )

    args = parser.parse_args()

    # Configure logging
    if args.verbose:
        logging.getLogger().setLevel(logging.DEBUG)

    # Validate arguments
    if not args.json_dir.exists():
        logger.error(f"JSON directory not found: {args.json_dir}")
        sys.exit(1)

    dry_run = not args.apply
    bucket_name = extract_bucket_name(args.bucket)

    try:
        # Step 1: Load JSON mappings
        logger.info("")
        logger.info("Step 1: Loading JSON mappings")
        logger.info("-" * 80)
        mapping = load_json_mapping(args.json_dir)

        if not mapping:
            logger.error("No image mappings found. Check your JSON files.")
            sys.exit(1)

        # Step 2: Initialize Firebase
        logger.info("")
        logger.info("Step 2: Initializing Firebase Admin SDK")
        logger.info("-" * 80)
        bucket = initialize_firebase(args.service_account, bucket_name)
        logger.info(f"Connected to bucket: {bucket_name}")

        # Step 3: Verify source images exist
        logger.info("")
        logger.info("Step 3: Verifying source images")
        logger.info("-" * 80)
        found_mapping, missing_paths = verify_source_images_exist(bucket, mapping, dry_run)

        # Step 4: Reorganize images
        logger.info("")
        logger.info("Step 4: Reorganizing images")
        logger.info("-" * 80)
        stats = reorganize_storage(bucket, found_mapping, dry_run)

        # Step 5: Print summary
        logger.info("")
        print_summary(stats, len(missing_paths), dry_run)

        # Exit codes
        if stats['failed'] > 0:
            logger.warning("Some images failed to copy. Check the log above.")
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
