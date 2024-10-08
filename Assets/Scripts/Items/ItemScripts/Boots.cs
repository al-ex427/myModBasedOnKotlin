using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietBSODA : Item
{
    public override void OnUse()
    {
        UnityEngine.Object.Instantiate<GameObject>(GameControllerScript.Instance.bsodaSpray, GameControllerScript.Instance.playerTransform.position, GameControllerScript.Instance.cameraTransform.rotation);
        ItemManager.Instance.ResetItem();
        GameControllerScript.Instance.player.ResetGuilt("drink", 1f);
        GameControllerScript.Instance.audioDevice.PlayOneShot(GameControllerScript.Instance.aud_Soda);
    }
}
