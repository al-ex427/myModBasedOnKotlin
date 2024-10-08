using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KOTLIN.Items.Custom
{
    [CreateAssetMenu()]
    public class ItemObject : ScriptableObject
    {
        [SerializeField]
        public Item ITMScript;

        public Sprite ItemSpriteBig;

        public Sprite ItemSpriteSmall;

        public string nameKey;

        [Header("Functions")]

        public Functions Happen;

        // Function stuff   

    }
    [System.Serializable]
    public struct Functions
    {
        [Header("Events")]
        public UnityEvent onUse;
        public UnityEvent onPickup;

        [Space()]
        public UnityEvent onSelected;
        public UnityEvent onDeselected;
    }
}
