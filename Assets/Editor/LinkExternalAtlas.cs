using UnityEngine;
using UnityEditor;
using TMPro;
using System.Reflection;

public class LinkExternalAtlas : EditorWindow
{
    private TMP_FontAsset fontAsset;
    private Texture2D atlasTexture;

    [MenuItem("Tools/Link External Atlas to Font")]
    static void Init()
    {
        LinkExternalAtlas window = (LinkExternalAtlas)EditorWindow.GetWindow(typeof(LinkExternalAtlas));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Link External Atlas Texture", EditorStyles.boldLabel);
        
        fontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", fontAsset, typeof(TMP_FontAsset), false);
        atlasTexture = (Texture2D)EditorGUILayout.ObjectField("Atlas Texture PNG", atlasTexture, typeof(Texture2D), false);

        if (GUILayout.Button("Link Atlas to Font"))
        {
            if (fontAsset == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select a Font Asset first!", "OK");
                return;
            }

            if (atlasTexture == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select an Atlas Texture PNG first!", "OK");
                return;
            }

            LinkAtlas();
        }
    }

    void LinkAtlas()
    {
        // Use reflection to access the private m_AtlasTexture field
        FieldInfo atlasField = typeof(TMP_FontAsset).GetField("m_AtlasTexture", BindingFlags.NonPublic | BindingFlags.Instance);
        
        if (atlasField != null)
        {
            atlasField.SetValue(fontAsset, atlasTexture);
            EditorUtility.SetDirty(fontAsset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Success", 
                "External atlas texture linked successfully!\n\n" +
                "The font should now persist correctly when you save the project.", 
                "OK");
            
            Debug.Log($"Successfully linked {atlasTexture.name} to {fontAsset.name}");
        }
        else
        {
            EditorUtility.DisplayDialog("Error", 
                "Could not access atlas texture field. This may be a TMP version compatibility issue.", 
                "OK");
        }
    }
}