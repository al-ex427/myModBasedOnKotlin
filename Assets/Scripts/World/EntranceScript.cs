using PrimeTween;
using UnityEngine;
using KOTLIN; 

public class EntranceScript : MonoBehaviour
{

    public void Lower(GameControllerScript gc)
    {
        if (decompType == DecompType.Classic)
        {
            base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
            if (gc.finaleMode)
            {
                this.wall.material = this.map;
            }
        }
        else if (decompType != DecompType.Classic && gc.notebooks >= gc.MaxNotebooks && !loweredGate)
        {
            loweredGate = true; 
            Vector3 nextPos = wall.transform.position - new Vector3(0f, 10f, 0f);
            PrimeTween.Tween.Position(wall.transform, nextPos, 1, PrimeTween.Ease.Linear); 
        }
    }

    public void Raise()
    {
        if (decompType != DecompType.Classic)
        {
            if (GetComponentInChildren<NearExitTriggerScript>().doorAnimator != null)
                GetComponentInChildren<NearExitTriggerScript>().doorAnimator.SetTrigger("OPEN");
            return; 
        }

        base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
    }

    private bool loweredGate; 

    public Material map;

    public MeshRenderer wall;
    [HideInInspector] public DecompType decompType;
}