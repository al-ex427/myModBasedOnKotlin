using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZestyBar : Item
{
    public override void OnUse()
    {
        GameControllerScript.Instance.player.stamina = GameControllerScript.Instance.player.maxStamina * 2f;
        ItemManager.Instance.ResetItem();
    }
}
