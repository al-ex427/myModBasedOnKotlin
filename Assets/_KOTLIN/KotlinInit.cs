using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOTLIN
{
    public class KotlinInit : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadEngine()
        {
            DontDestroyOnLoad(Instantiate(Resources.Load<GameObject>("KotlinManager")));
        }
    }

}