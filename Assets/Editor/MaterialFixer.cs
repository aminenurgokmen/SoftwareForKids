using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialFixer : EditorWindow
{
    [MenuItem("Tools/Fix Kitchen Materials")]
    static void FixMaterials()
    {
        string materialsPath = "Assets/Package04_Free_Kitchen/FreeKitchen/Materials/Version01";
        string texturesPath = "Assets/Package04_Free_Kitchen/FreeKitchen/Textures/Version01";

        string[] materialGuids = AssetDatabase.FindAssets("t:Material", new[] { materialsPath });

        foreach (string guid in materialGuids)
        {
            string materialPath = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            if (material != null)
            {
                string materialName = material.name;
                string albedoTextureName = materialName + "_AlbedoTransparency.png";
                string albedoTexturePath = Path.Combine(texturesPath, albedoTextureName).Replace("\\", "/");

                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(albedoTexturePath);

                if (texture != null)
                {
                    material.SetTexture("_BaseMap", texture);
                    EditorUtility.SetDirty(material);
                    Debug.Log($"Fixed {materialName}: assigned {albedoTextureName}");
                }
                else
                {
                    Debug.LogWarning($"Texture not found for {materialName}: {albedoTexturePath}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Material fixing complete!");
    }
}