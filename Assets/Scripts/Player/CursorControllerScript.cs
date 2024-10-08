﻿using Pixelplacement;
using UnityEngine;

public class CursorControllerScript : Singleton<CursorControllerScript>
{
    private void Update()
    {
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor(prevent it from moving)
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor(allow it to move)
        Cursor.visible = true;
    }
}