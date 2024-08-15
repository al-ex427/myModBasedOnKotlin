using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour
{
    public void OnUse()
    {
        base.StartCoroutine(GameControllerScript.Instance.BootAnimation());
        GameControllerScript.Instance.player.ActivateBoots();
        ItemManager.Instance.ResetItem();
    }
}
