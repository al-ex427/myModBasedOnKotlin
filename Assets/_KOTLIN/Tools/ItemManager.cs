using KOTLIN.Items;
using KOTLIN.Translation;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace KOTLIN.Items
{
    #region Item Struct
    [System.Serializable]
    public struct Item
    {
        public string NameKey;
        public Texture ItemSprite;

        [Header("Events")]
        public UnityEvent OnUse;
        public UnityEvent OnPickup;

        [Space()]
        public UnityEvent OnSelect;
        public UnityEvent OnDeselect; 
    }
    #endregion

    public class ItemManager : Singleton<ItemManager>
    {
        #region Variables
        public int itemSelected;
        public Item[] items;
        public int[] item = new int[0];
        private int[] itemSelectOffset;
        public RawImage[] itemSlot = new RawImage[3];
        public KeyCode[] numericKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
        [SerializeField] private RectTransform itemSelect;
        [SerializeField] private TMP_Text itemText;
        #endregion

        #region Init
        private void Start()
        {
            int[] array = new int[itemSlot.Length];
            for (int i = 0; i < itemSlot.Length; i++)
            {
                array[i] = (int)itemSlot[i].rectTransform.anchoredPosition.x;
            }

            Array.Resize(ref item, itemSlot.Length);
            Array.Resize(ref numericKeys, array.Length);
            this.itemSelectOffset = array;
        }
        #endregion

        #region Editor Updates
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                Debug.Log("validated");
                if (itemSelectOffset == null || itemSelectOffset.Length == 0 || itemSlot == null || itemSlot.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < itemSlot.Length; i++)
                {
                    int itemId = item[i];
                    if (itemId >= 0 && itemId < items.Length)
                    {
                        itemSlot[i].texture = items[itemId].ItemSprite;
                    }
                    else
                    {
                        itemSlot[i].texture = null;
                    }
                }
                UpdateItemSelection();
            }
        }

        public void EditorUpdate()
        {
            OnValidate(); 
        }

        #endregion


        #region Input Checks 
        private void Update()
        {
            if (Time.timeScale != 0f)
            {
                for (int i = 0; i < numericKeys.Length; ++i)
                {
                    if (Input.GetKeyDown(numericKeys[i]))
                    {
                        this.itemSelected = i;
                        this.UpdateItemSelection();
                        break;
                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    this.DecreaseItemSelection();
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    this.IncreaseItemSelection();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    this.UseItem();
                }
            }
        }
        #endregion

        #region Item Selection
        private void IncreaseItemSelection()
        {
            items[item[itemSelected]].OnDeselect?.Invoke();
            this.itemSelected++;
            if (this.itemSelected > itemSlot.Length - 1)
            {
                this.itemSelected = 0;
            }

            items[item[itemSelected]].OnSelect?.Invoke();
            this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], itemSelect.anchoredPosition.y, 0f); //Moves the item selector background(the red rectangle)
            this.UpdateItemName();
        }

        private void DecreaseItemSelection()
        {
            items[item[itemSelected]].OnDeselect?.Invoke();
            this.itemSelected--;
            if (this.itemSelected < 0)
            {
                this.itemSelected = itemSlot.Length - 1;
            }

            items[item[itemSelected]].OnSelect?.Invoke();
            this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], itemSelect.anchoredPosition.y, 0f); //Moves the item selector background(the red rectangle)
            this.UpdateItemName();
        }
        #endregion

        #region Item Collection & Use
        public void CollectItem(int item_ID)
        {
            int emptySlotIndex = -1;
            for (int i = 0; i < this.item.Length; i++)
            {
                if (this.item[i] == 0)
                {
                    emptySlotIndex = i;
                    break;
                }
            }

            int slotIndex = emptySlotIndex != -1 ? emptySlotIndex : this.itemSelected;

            this.item[slotIndex] = item_ID;
            this.itemSlot[slotIndex].texture = items[item_ID].ItemSprite;

            items[this.item[itemSelected]].OnPickup?.Invoke();
            this.UpdateItemName();
        }

        private void UseItem()
        {
            if (this.item[this.itemSelected] != 0)
            {
                items[this.item[itemSelected]].OnUse?.Invoke();
            }
        }
        #endregion

        #region Item Removing
        public void ResetItem()
        {
            this.item[this.itemSelected] = 0;
            this.itemSlot[this.itemSelected].texture = items[0].ItemSprite;
            this.UpdateItemName();
        }

        public void LoseItem(int id)
        {
            this.item[id] = 0;
            this.itemSlot[id].texture = items[0].ItemSprite;
            this.UpdateItemName();
        }
        #endregion

        #region Item Info Updates
        public void UpdateItemSelection()
        {
            this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], itemSelect.anchoredPosition.y, 0f); //Moves the item selector background(the red rectangle)
            this.UpdateItemName();
        }

        private void UpdateItemName()
        {
            this.itemText.text = TranslationManager.Instance.GetTranslationString(items[this.item[this.itemSelected]].NameKey);
        }
        #endregion
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ItemManager))]
public class ItemManagerEditor : Editor
{
    SerializedProperty itemsProp;
    SerializedProperty itemProp;
    SerializedProperty itemSlotProp;
    SerializedProperty numericKeysProp;
    SerializedProperty itemSelectProp;
    SerializedProperty itemTextProp;

    private void OnEnable()
    {
        itemsProp = serializedObject.FindProperty("items");
        itemProp = serializedObject.FindProperty("item");
        itemSlotProp = serializedObject.FindProperty("itemSlot");
        numericKeysProp = serializedObject.FindProperty("numericKeys");
        itemSelectProp = serializedObject.FindProperty("itemSelect");
        itemTextProp = serializedObject.FindProperty("itemText");
    }

    public override void OnInspectorGUI()
    {
        ItemManager itemManager = (ItemManager)target;

        serializedObject.Update();

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Selected Item Index", GUILayout.Width(150));
        int oldItemSelected = itemManager.itemSelected;
        int newItemSelected = EditorGUILayout.IntSlider(itemManager.itemSelected + 1, 1, itemManager.itemSlot.Length) - 1;
        if (newItemSelected != oldItemSelected)
        {
            Undo.RecordObject(itemManager, "Changed Selected Item Index");
            itemManager.itemSelected = newItemSelected;
            itemManager.EditorUpdate(); //editor update is VERY bruteforce but FUCK YOU
            EditorUtility.SetDirty(itemManager); 
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Item Entries", EditorStyles.boldLabel);

        string[] itemNames = new string[itemManager.items.Length];
        for (int i = 0; i < itemManager.items.Length; i++)
        {
            itemNames[i] = $"{i}: {itemManager.items[i].NameKey}";
        }

        for (int i = 0; i < itemManager.item.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Slot {i + 1}", GUILayout.Width(100));

            int itemId = itemManager.item[i];
            int selectedIndex = Mathf.Max(itemId, 0);
            int newSelectedIndex = EditorGUILayout.Popup(selectedIndex, itemNames);

            if (newSelectedIndex != itemId)
            {
                Undo.RecordObject(itemManager, $"Changed Item {i}");
                itemManager.item[i] = newSelectedIndex;
                itemManager.EditorUpdate(); 
                EditorUtility.SetDirty(itemManager);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(itemSlotProp, new GUIContent("Item Slots"));
        EditorGUILayout.PropertyField(numericKeysProp, new GUIContent("Numeric Keys"));
        EditorGUILayout.PropertyField(itemSelectProp, new GUIContent("Item Select"));
        EditorGUILayout.PropertyField(itemTextProp, new GUIContent("Item Text"));

        EditorGUILayout.EndVertical();
    }
}
#endif
