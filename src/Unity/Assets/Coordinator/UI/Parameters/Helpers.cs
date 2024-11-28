using System;
using UnityEngine;

public static class Helpers
{
    public static void DoOrComplainIfNull<T>(T value, string varName, Action action)
    {
        if (value == null)
        {
            Debug.LogWarning($"The variable '{varName}' is not set.");
        }
        else
        {
            action();
        }
    }

    public static void DoOrErrorIfNull<T>(T value, string varName, Action action)
    {
        if (value == null)
        {
            Debug.LogError($"The variable '{varName}' is not set.");
            throw new ArgumentNullException(varName);
        }
        else
        {
            action();
        }
    }
}
