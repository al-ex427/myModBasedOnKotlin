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
            UpdateLights(null);
        }

        public void UpdateLights(Texture2D newMap)
        {
            if (newMap != null)
                Lightmap = newMap;

            Shader.SetGlobalTexture("_Lightmap", Lightmap);
        }
    }

}