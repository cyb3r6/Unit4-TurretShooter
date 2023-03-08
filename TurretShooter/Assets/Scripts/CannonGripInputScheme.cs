using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGripInputScheme : ICannonInputScheme
{
    public bool FireTriggered()
    {
        return Input.GetButtonDown("RightPrimaryButton");
    }

    public Vector2 AimInput()
    {
        throw new System.NotImplementedException();
    }

    public void Dispose() { }

}
