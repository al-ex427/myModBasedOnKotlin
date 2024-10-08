using Pixelplacement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    private void Update()
    {
        if (this.usingJoystick & EventSystem.current.currentSelectedGameObject == null)
        {
            if (!Singleton<GameControllerScript>.Instance.mouseLocked)
            {
                Singleton<GameControllerScript>.Instance.LockMouse();
            }
        }
        else if (!this.usingJoystick && Singleton<GameControllerScript>.Instance.mouseLocked)
        {
            Singleton<GameControllerScript>.Instance.UnlockMouse();
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