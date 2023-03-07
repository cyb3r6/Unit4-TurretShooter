using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICannonInputScheme
{
    bool FireTriggered();
    Vector2 AimInput();
    void Dispose();
}
