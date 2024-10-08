using KOTLIN.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : Item
{ 
    public override void OnUse()
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameControllerScript.Instance.alarmClock, GameControllerScript.Instance.playerTransform.position, GameControllerScript.Instance.cameraTransform.rotation);
        gameObject.GetComponent<AlarmClockScript>().baldi = GameControllerScript.Instance.baldiScrpt;
        ItemManager.Instance.ResetItem();
    }
}
