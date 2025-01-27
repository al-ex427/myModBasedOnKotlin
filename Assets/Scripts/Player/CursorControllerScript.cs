using Pixelplacement;
//using System.ComponentModel;
using UnityEngine;
public class CursorControllerScript : Singleton<CursorControllerScript>
{

    [Header("Data")]

    [SerializeField]
    [ReadOnly] public bool cursorLocked;
    private void Update()
    {
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor(prevent it from moving)
        Cursor.visible = false;
        cursorLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor(allow it to move)
        Cursor.visible = true;
        cursorLocked = false;
    }
}