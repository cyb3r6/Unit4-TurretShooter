using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonKeyboardInputScheme : ICannonInputScheme
{
    public bool FireTriggered()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public Vector2 AimInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void Dispose() { }
}
