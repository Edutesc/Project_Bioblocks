# Firebase Storage Image Reorganization

This guide walks you through reorganizing your Firebase Storage images from scattered paths into a clean `Question/Question{N}/` structure.

## Prerequisites

1. **Python 3.7+** installed
2. **firebase-admin SDK** - will be installed automatically by the script
3. **Firebase credentials** - either:
   - Service account JSON file, OR
   - Firebase CLI configured locally (`gcloud auth application-default login`)
4. **Exported JSON files** - from the DatabaseExporter (already done)

## Understanding the Reorganization

**Before:**
```
AnswerImages/
  carbohydrates_image_001.png
  carbohydrates_image_002.png
  carbohydrates_answer_a_001.png
  ...

QuestionImages/
  enzyme_question_1.png
  enzyme_question_2.png
  ...
```

**After:**
```
Question/
  Question1/
    enzyme_question_1.png
    enzyme_answer_a_1.png
  Question2/
    enzyme_question_2.png
    enzyme_answer_b_2.png
  ...
```

## Getting Your Firebase Service Account

If you're using Firebase CLI locally, you likely already have credentials. If you need a service account JSON file:

1. Go to [Firebase Console](https://console.firebase.google.com)
2. Select your project → Project Settings (gear icon)
3. Service Accounts tab
4. Click "Generate New Private Key"
5. Save the JSON file somewhere safe (e.g., `~/.firebase/microlearning-dev-serviceAccountKey.json`)

## Running the Reorganization

### Step 1: Preview Changes (Dry Run)

```bash
cd /Users/lucianopuzer/Developing/Project_Bioblocks

# Option A: Using the shell script
bash run_reorganize.sh

# Option B: Direct Python (more control)
python3 reorganize_firebase_storage.py \
  --json-dir "/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings" \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --service-account "/path/to/serviceAccountKey.json" \
  --verbose
```

**Expected output:**
```
[INFO] Step 1: Loading JSON mappings
[INFO] Found 10 JSON file(s)...
[INFO] Mapping complete: 108 images to reorganize

[INFO] Step 2: Initializing Firebase Admin SDK
[INFO] Connected to bucket: microlearning-dev-79c0c.appspot.com

[INFO] Step 3: Verifying source images
[INFO]   ✓ Found: QuestionImages/enzyme_question_1.png
[INFO]   ✓ Found: AnswerImages/enzyme_answer_a_1.png
[INFO] Verification complete:
[INFO]   108 images found
[INFO]   0 images not found

[INFO] Step 4: Reorganizing images
[INFO] [DRY RUN MODE - No changes will be made]
[INFO] [1/108] Processing...
[INFO]   [DRY RUN] Would copy: QuestionImages/enzyme_question_1.png -> Question/Question1/enzyme_question_1.png
...

[INFO] SUMMARY
[INFO] Total images mapped:    108
[INFO] Successfully copied:    108
[INFO] This was a DRY RUN. To apply changes, run without --dry-run flag.
```

### Step 2: Apply Changes

Once you've verified the dry run looks correct, apply the changes:

```bash
# Option A: Using the shell script
bash run_reorganize.sh --apply

# Option B: Direct Python
python3 reorganize_firebase_storage.py \
  --json-dir "/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings" \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --service-account "/path/to/serviceAccountKey.json" \
  --apply \
  --verbose
```

This will:
- Download each image from its source location
- Upload to the new `Question/Question{N}/` location
- Show progress for each image

## Troubleshooting

### "Service account file not found"

Provide the correct path to your service account JSON:

```bash
python3 reorganize_firebase_storage.py \
  --json-dir "..." \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --service-account "$HOME/.firebase/serviceAccountKey.json"
```

### "Could not initialize Firebase"

Try using your existing Firebase CLI setup:

```bash
# Set up Firebase CLI credentials
gcloud auth application-default login

# Then run without --service-account (it will use the credentials above)
python3 reorganize_firebase_storage.py \
  --json-dir "..." \
  --bucket "gs://microlearning-dev-79c0c.appspot.com"
```

### "Images not found" warning

This means the JSON files have paths that don't exist in Firebase Storage. Possible causes:

1. **Path typo** in the question database
2. **Image not yet uploaded** to Firebase Storage
3. **Different path structure** than expected

Check:
- Are the images in their current Firebase Storage folders?
- Do the paths in the JSON match the actual Firebase Storage paths?

You can see missing images in the script output:
```
[WARNING]   ✗ Not found: AnswerImages/some_image.png
```

### Images copying slowly

The script copies images one-by-one by default. Adjust your network patience—downloading and re-uploading 200+ images may take several minutes depending on image size and network speed.

## Next Steps

After reorganization completes:

1. **Verify in Firebase Console** - Check that `Question/Question{N}/` folders exist with the correct images
2. **Update your app code** - Modify `FirebaseStorageImageRepository.ROOT_PATH` from `"questions"` to `"Question"` (or adjust path references)
3. **Test lazy loading** - Run your app and confirm images load from the new structure
4. **Optional: Clean up old folders** - Delete `AnswerImages/` and `QuestionImages/` folders if no longer needed

## Monitoring the Process

To watch the reorganization in real-time:

1. Open [Firebase Console](https://console.firebase.google.com) → Storage
2. Navigate to your bucket
3. Refresh occasionally to see `Question/` folder being populated

Estimated time: ~5-10 minutes for 200-300 images (depending on size and network)

## Questions?

The script includes detailed logging. Run with `--verbose` to see debug-level information:

```bash
python3 reorganize_firebase_storage.py \
  --json-dir "..." \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --verbose
```

---

**Important:** Always run with `--dry-run` (default) first to verify everything looks correct before applying changes!
