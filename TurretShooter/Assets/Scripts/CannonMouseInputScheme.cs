using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMouseInputScheme : ICannonInputScheme
{
    public CannonMouseInputScheme()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool FireTriggered()
    {
        return Input.GetButtonDown("Fire1");
    }

    public Vector2 AimInput()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public void Dispose()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
