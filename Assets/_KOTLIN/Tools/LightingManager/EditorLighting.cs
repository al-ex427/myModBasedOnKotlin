#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using KOTLIN.UI;
using KOTLIN.UI.States; 

public class EditorLighting : EditorWindow
{
    private KOTLIN_UIStates stateMan;

    [MenuItem("KOTLIN/Fix Lighting")]
    static void LoadWindow()
    {
        KOTLIN_UIBASE.createWindow<EditorLighting>("Lighting Fix"); 
    }

    private void OnEnable()
    {
        FixLightingWindow window = new FixLightingWindow();
        string assetPath = EditorPrefs.GetString("LightmapPath", "");
        if (!string.IsNullOrEmpty(assetPath))
        {
            Texture2D lightmap = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (lightmap != null)
            {
                 window.Lightmap = lightmap;
            }
        }

        stateMan = new KOTLIN_UIStates(new FixLightingWindow()); 
    }

    private void OnGUI()
    {
        stateMan.RenderCurrentState(); 
    }

    public class FixLightingWindow : IUIState
    {
        public Texture2D Lightmap; 

        public void RenderUI()
        {
            Lightmap = (Texture2D)EditorGUILayout.ObjectField(Lightmap, typeof(Texture2D), false); 

            if (Lightmap != null && GUILayout.Button("Apply"))
            {
                Shader.SetGlobalTexture("_GlobalLights", Lightmap);
                EditorPrefs.SetString("LightmapPath", AssetDatabase.GetAssetPath(Lightmap));
            }
        }
    }

    [InitializeOnLoadMethod]
    private static void LoadLightmap()
    {
        string assetPath = EditorPrefs.GetString("LightmapPath", "");
        if (!string.IsNullOrEmpty(assetPath))
        {
            Texture2D lightmap = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (lightmap != null)
            {
                Shader.SetGlobalTexture("_GlobalLights", lightmap);
            }
        }
    }
}
#endif 