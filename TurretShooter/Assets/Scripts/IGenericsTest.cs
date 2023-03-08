using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGenericsTest
{
    void Log<T>(T score);
}
