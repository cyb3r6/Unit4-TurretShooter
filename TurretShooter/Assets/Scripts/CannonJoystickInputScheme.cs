using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonJoystickInputScheme : ICannonInputScheme
{
    public bool FireTriggered()
    {
        return Input.GetButtonDown("RightPrimaryButton");
    }

    public Vector2 AimInput()
    {
        return new Vector2(Input.GetAxis("RightJoystickX"), Input.GetAxis("RightJoystickY"));
    }

    public void Dispose() { }
}
