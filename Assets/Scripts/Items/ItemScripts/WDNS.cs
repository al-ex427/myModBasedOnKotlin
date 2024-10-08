using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDNS : Item
{
    public override void OnUse()
    {
        Ray ray5 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit5;
        if (Physics.Raycast(ray5, out raycastHit5) && (raycastHit5.collider.tag == "Door" & Vector3.Distance(GameControllerScript.Instance.playerTransform.position, raycastHit5.transform.position) <= 10f))
        {
            raycastHit5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor();
            ItemManager.Instance.ResetItem();
            GameControllerScript.Instance.audioDevice.PlayOneShot(GameControllerScript.Instance.aud_Spray);
        }
    }
}
