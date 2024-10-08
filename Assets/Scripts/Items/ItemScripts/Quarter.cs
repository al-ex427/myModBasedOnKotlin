using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarter : Item
{
    //NOTE TO SELF: CLEAN THIS UP YOU LOSER (bfb)
    public override void OnUse()
    {
        Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit3;
        if (Physics.Raycast(ray3, out raycastHit3))
        {
            if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                ItemManager.Instance.ResetItem();
                ItemManager.Instance.CollectItem(1);
            }
            else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                ItemManager.Instance.ResetItem();
                ItemManager.Instance.CollectItem(2);
            }
            else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit3.transform.position) <= 10f)
            {
                raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
                ItemManager.Instance.ResetItem();
            }
        }
    }
}