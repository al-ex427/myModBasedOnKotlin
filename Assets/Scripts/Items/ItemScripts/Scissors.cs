using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Item
{
    public override void OnUse()
    {
        Ray ray6 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit6;
        if (GameControllerScript.Instance.player.jumpRope)
        {
            GameControllerScript.Instance.player.DeactivateJumpRope();
            GameControllerScript.Instance.playtimeScript.Disappoint();
            ItemManager.Instance.ResetItem();
        }
        else if (Physics.Raycast(ray6, out raycastHit6) && raycastHit6.collider.name == "1st Prize")
        {
            GameControllerScript.Instance.firstPrizeScript.GoCrazy();
            ItemManager.Instance.ResetItem();
        }
    }
}
