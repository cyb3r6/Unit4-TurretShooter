using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    private static readonly Dictionary<int, object> serviceMap;

    static GameServices()
    {
        serviceMap = new Dictionary<int, object>();
    }

    public static void RegisterService<T>(T service) where T : class
    {
        serviceMap[GetId<T>()] = service;
    }

    public static void DeregisterService<T>(T service) where T : class
    {
        serviceMap.Remove(GetId<T>());
    }

    public static T GetService<T>() where T : class
    {
        Debug.Assert(serviceMap.ContainsKey(GetId<T>()), "Trying to get inexistent service!");

        return (T)serviceMap[GetId<T>()];
    }

    public static void Clear()
    {
        serviceMap.Clear();
    }

    private static int GetId<T>()
    {
        return typeof(T).GetHashCode();
    }
}
