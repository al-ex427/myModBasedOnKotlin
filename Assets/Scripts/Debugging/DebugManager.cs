namespace al_ex427.Debugging
{
    using Pixelplacement;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;

    [System.Serializable]
    public struct DebugSettings
    {
        public bool Invincible;

        public bool NoClip;

        public bool SuperSpeed;
    }
    public class DebugManager : Singleton<DebugManager>
    {
        [Header("Data")]

        public PlayerScript player;

        [SerializeField]
        private DebugSettings debugSettings;

        private RenderDebugUI debugManUi;

        [SerializeField]
        public bool DebugOn;

        

        private void Awake()
        {
            RenderDebugUI();

            if (!DebugOn)
            {
                Debug.LogWarning("Debug is not on; Destroying the manager.");
                Destroy(base.gameObject);
                return;
            }
        }
        private void RenderDebugUI()
        {
            void OnGui()
            {
               
                GUI.Box(new Rect(10, 150, 155, 70), "Invincible = False\nNoClip = False\nSuper Speed = False");
                Debug.Log("Built the UI");
            }
            
        }
       
    }
    public class RenderDebugUI : MonoBehaviour
    {
        void OnGUI()
        {
            

        }
    }
}