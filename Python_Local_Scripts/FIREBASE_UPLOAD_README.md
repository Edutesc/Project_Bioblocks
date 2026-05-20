# Upload Local Images to Firebase Storage

This guide walks you through uploading your local app images to Firebase Storage with automatic organization.

## What This Does

Takes your images from `Assets/Resources/` and uploads them to Firebase Storage organized by question:

**Local Structure:**
```
Assets/Resources/
  AnswerImages/
    ProteinDB/
      proteinQuestion_1.png
    AminoacidsDB/
      alanina.png
      ...
  QuestionImages/
    EnzymeDB/
      enzymeDB_ImageQuestionContainer12.png
    ...
```

**Firebase Storage Result:**
```
Question/
  Question1/
    proteinQuestion_1.png
  Question3/
    treonina.png
    glicina.png
    histidina.png
    alanina.png
  Question7/
    ...
```

## Prerequisites

1. **Python 3.7+** installed
2. **firebase-admin SDK** - will be installed automatically
3. **Service account JSON** - from Firebase Console (see below)
4. **Exported JSON files** - from DatabaseExporter (already done)

## Getting Firebase Service Account

1. Go to [Firebase Console](https://console.firebase.google.com)
2. Select your **microlearning** project
3. Click gear icon (⚙️) → **Project Settings**
4. Go to **Service Accounts** tab
5. Click **Generate New Private Key**
6. Save to your home directory:

```bash
# Move downloaded file
mv ~/Downloads/microlearning-*.json ~/firebase-key.json
```

## Running the Upload

### Step 1: Preview (Dry Run)

First, see what will be uploaded without actually doing it:

```bash
cd /Users/lucianopuzer/Developing/Project_Bioblocks

# Using the shell script (recommended)
bash run_upload.sh

# OR direct Python
python3 upload_images_to_firebase.py \
  --json-dir "/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings" \
  --resources-dir "/Users/lucianopuzer/Developing/Project_Bioblocks/Assets/Resources" \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --service-account ~/firebase-key.json \
  --verbose
```

**Expected output:**
```
[INFO] Step 1: Loading JSON mappings and finding local images
[INFO] Found 10 JSON file(s)
[INFO] Processing AcidBaseBufferQuestionDatabase.json: 9 questions
[INFO]   Q7: AcidBaseBufferDB_ImageQuestionContainer7 → Question/Question7/...
[INFO] Mapping complete: 164 images ready to upload

[INFO] Step 2: Initializing Firebase Admin SDK
[INFO] Connected to bucket: microlearning-dev-79c0c.appspot.com

[INFO] Step 3: Uploading images
[INFO] [DRY RUN] Would upload: AcidBaseBufferDB_ImageQuestionContainer7 (0.25 MB) → Question/Question7/...
...

[INFO] SUMMARY
[INFO] Total images to upload:    164
[INFO] Successfully uploaded:     164
[INFO] This was a DRY RUN. To apply changes, run without --dry-run flag.
```

### Step 2: Actually Upload

Once the dry run looks good, upload for real:

```bash
# Using the shell script
bash run_upload.sh --apply

# OR direct Python
python3 upload_images_to_firebase.py \
  --json-dir "/Users/lucianopuzer/Library/Application Support/halbus/BioBlocks/ImageMappings" \
  --resources-dir "/Users/lucianopuzer/Developing/Project_Bioblocks/Assets/Resources" \
  --bucket "gs://microlearning-dev-79c0c.appspot.com" \
  --service-account ~/firebase-key.json \
  --apply \
  --verbose
```

This will:
- Upload each image from local → Firebase
- Organize into `Question/Question{N}/` structure
- Show progress for each upload
- Take 5-10 minutes depending on network

## Troubleshooting

### "Images not found locally"

The script couldn't find some image files. This means either:

1. **Images don't exist** where the JSON says they should
2. **Path typos** in your question databases
3. **Missing file extensions** in JSON paths

The script will list them:
```
[WARNING] MISSING FILES (not found locally)
[WARNING]   AnswerImages/ProteinDB/some_image.png (Q28 answer image)
```

Check if those files exist in `Assets/Resources/` and verify the paths in your C# databases.

### "Could not initialize Firebase"

You didn't provide credentials. Make sure:
```bash
# Check service account file exists
ls -la ~/firebase-key.json

# Run with explicit path
python3 upload_images_to_firebase.py \
  ... \
  --service-account ~/firebase-key.json
```

### Upload is slow

Normal! Uploading 164 images (15MB) with network I/O takes time. The script shows progress for each file, so you can watch it work.

## After Upload

### 1. Verify in Firebase Console
- Open [console.firebase.google.com](https://console.firebase.google.com)
- Go to Storage
- Check that `Question/` folder exists with numbered subfolders

### 2. Update Your App Code

Your app currently tries to load images from paths like `"AnswerImages/ProteinDB/..."`

You need to update the ROOT_PATH in `FirebaseStorageImageRepository.cs`:

**Before:**
```csharp
private const string ROOT_PATH = "questions";  // or wherever they currently are
```

**After:**
```csharp
private const string ROOT_PATH = "Question";
```

Or adjust your image reference paths in the databases to use the new structure.

### 3. Test Lazy Loading

Once updated, test your app:
- Launch app → Login/Register → Should start prewarming images
- Images should load progressively as you answer questions
- Check Firebase usage in Console → Storage to see bandwidth

## Image Search Strategy

The script tries multiple strategies to find images:

1. **Exact path match** - `AnswerImages/ProteinDB/alanina`
2. **Filename search** - if path is just `alanina`, searches for any file named `alanina` in Resources
3. **Extension fallback** - if no extension provided, tries `.png`, `.jpg`, `.jpeg`, `.gif`, `.webp`

This makes it flexible if your local structure doesn't exactly match the JSON paths.

## Monitoring Progress

Watch the upload in real-time:

```bash
# In another terminal, watch Firebase
open "https://console.firebase.google.com/project/microlearning-dev-79c0c/storage"

# Refresh browser every 30 seconds to see progress
```

## Questions?

Run with `--verbose` flag to see detailed debug logs:

```bash
python3 upload_images_to_firebase.py ... --verbose
```

---

**Important:** Always run with `--dry-run` (default) first to verify everything looks correct!
