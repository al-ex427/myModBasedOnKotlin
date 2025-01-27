using Pixelplacement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    private void Update()
    {
        if (this.usingJoystick & EventSystem.current.currentSelectedGameObject == null)
        {
            if (!Singleton<CursorControllerScript>.Instance.cursorLocked)
            {
                Singleton<CursorControllerScript>.Instance.LockCursor();
            }
        }
        else if (!this.usingJoystick && Singleton<CursorControllerScript>.Instance.cursorLocked)
        {
            Singleton<CursorControllerScript>.Instance.UnlockCursor();
        }
    }

    private bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}