using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

namespace KOTLIN.Items
{
    [System.Serializable]
    public struct Item
    {
        public string NameKey;
        public Texture ItemSprite;
        public UnityEvent OnUse;
        public UnityEvent OnPickup;
    }

    public class ItemManager : MonoBehaviour
    {
        public Item[] items;
    }

}