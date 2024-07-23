using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOTLIN.Lighting
{
    public class LightingManager : MonoBehaviour
    {
        public Texture2D Lightmap;

        private void Start()
        {
            Shader.SetGlobalTexture("_PlaceholderSpecular", Lightmap);
            UpdateLights(Lightmap);
        }

        public void UpdateLights(Texture2D newMap)
        {
            if (newMap != null)
                Lightmap = newMap;

            Shader.SetGlobalTexture("_GlobalLights", Lightmap);
        }
    }

}