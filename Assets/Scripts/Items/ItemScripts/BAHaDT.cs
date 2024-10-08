using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAHaDT : Item
{
    public override void OnUse()
    {
        Ray ray4 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit4;
        if (Physics.Raycast(ray4, out raycastHit4) && (raycastHit4.collider.name == "TapePlayer" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit4.transform.position) <= 10f))
        {
            raycastHit4.collider.gameObject.GetComponent<TapePlayerScript>().Play();
            ItemManager.Instance.ResetItem();
        }
    }
}