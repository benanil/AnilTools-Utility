
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public static class Debug2
{
    #if UNITY_EDITOR
    private static List<string> logdata = new List<string>();
    private static bool messaged;
#endif

    public static void Log(object message)
    {
        #if UNITY_EDITOR
        Debug.Log(message);
        #endif
    }

    public static void LogSlow(object message,int speed)
    {
#if UNITY_EDITOR
        UniTask.Create(async () =>
        {
            if (messaged == false)
            {
                Debug.Log(message);
                messaged = true;
                await UniTask.Delay(speed,true);
                messaged = false;
            }
        });
#endif
    }

    public static void Log(object message , Color Color)
    {
    #if UNITY_EDITOR
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(Color.r * 255f), (byte)(Color.g * 255f), (byte)(Color.b * 255f), message));
    #endif
    }

    /// <summary>
    /// mesaj en fazla 1 kez yazılır
    /// </summary>
    public static void LogOnce(object message)
    {
#if UNITY_EDITOR
        Color color = Color.red;
        if (!logdata.Contains(message.ToString()))
        {
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
            logdata.Add(message.ToString());
        }
#endif
    }

    public static void LogBlue(object message)
    {
#if UNITY_EDITOR
        var color = Color.blue;
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
#endif
    }

    public static void LogWarning(object message)
    {
        #if UNITY_EDITOR
        Debug.LogWarning(message);
        #endif
    }

    public static void LogWarning(object message, Color color)
    {
    #if UNITY_EDITOR
        Debug.LogWarning(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
    #endif
    }

    public static void LogError(object message)
    {
        #if UNITY_EDITOR
        Debug.LogError(message);
        #endif
    }

    public static void LogError(object message,Color color)
    {
    #if UNITY_EDITOR
        Debug.LogError(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
    #endif
    }
}