using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericsTest : MonoBehaviour, IGenericsTest
{
    private void Awake()
    {
        CustomAddComponent<Target>(gameObject);
        CustomAddComponent<Target>(gameObject);
        CustomAddComponent<Target>(gameObject);
    }

    public void Log<T>(T score)
    {
        Debug.Log($"GenericsTest {score}");
    }

    private void LogComponent<T>(T component) where T : Component
    {
        Debug.Log($"GenericsTest {typeof(T)} | {component}");
    }

    private void CustomAddComponent<T>(GameObject obj) where T : MonoBehaviour
    {
        var component = obj.AddComponent<T>();
        component.enabled = false;
        Debug.Log($"GenericsTest {typeof(T)} | {obj.GetComponents<T>().Length}");
    }
}
