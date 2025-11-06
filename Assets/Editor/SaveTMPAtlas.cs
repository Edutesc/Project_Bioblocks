using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;

public class SaveTMPAtlas : EditorWindow
{
    private TMP_FontAsset fontAsset;

    [MenuItem("Tools/Save TMP Atlas Texture")]
    static void Init()
    {
        SaveTMPAtlas window = (SaveTMPAtlas)EditorWindow.GetWindow(typeof(SaveTMPAtlas));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Save TextMesh Pro Atlas Texture", EditorStyles.boldLabel);
        
        fontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", fontAsset, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Save Atlas as PNG"))
        {
            if (fontAsset == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select a Font Asset first!", "OK");
                return;
            }

            if (fontAsset.atlasTexture == null)
            {
                EditorUtility.DisplayDialog("Error", "Font Asset has no atlas texture!", "OK");
                return;
            }

            SaveAtlasTexture();
        }
    }

    void SaveAtlasTexture()
    {
        // Get the atlas texture
        Texture2D atlasTexture = fontAsset.atlasTexture;

        // Create a readable copy
        Texture2D readableTexture = new Texture2D(atlasTexture.width, atlasTexture.height, TextureFormat.RGBA32, false);
        
        // Use RenderTexture to copy the texture
        RenderTexture rt = RenderTexture.GetTemporary(atlasTexture.width, atlasTexture.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(atlasTexture, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;
        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        // Encode to PNG
        byte[] bytes = readableTexture.EncodeToPNG();

        // Get the path of the font asset
        string fontAssetPath = AssetDatabase.GetAssetPath(fontAsset);
        string directory = Path.GetDirectoryName(fontAssetPath);
        string fileName = fontAsset.name + " Atlas.png";
        string savePath = Path.Combine(directory, fileName);

        // Save the PNG file
        File.WriteAllBytes(savePath, bytes);
        
        // Refresh the asset database
        AssetDatabase.Refresh();

        // Import the texture with correct settings
        TextureImporter importer = AssetImporter.GetAtPath(savePath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Default;
            importer.isReadable = true;
            importer.mipmapEnabled = false;
            importer.SaveAndReimport();
        }

        EditorUtility.DisplayDialog("Success", $"Atlas texture saved to:\n{savePath}\n\nNow manually assign this texture to your Font Asset:\n1. Select the Font Asset\n2. In Inspector, find 'Atlas Texture'\n3. Drag the saved PNG into that field", "OK");
        
        Debug.Log($"Atlas texture saved successfully to: {savePath}");
    }
}