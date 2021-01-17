using UnityEngine;

namespace AnilTools
{
    public static class Vibration
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    public static readonly AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static readonly AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static readonly AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
        public static AndroidJavaClass unityPlayer;
        public static AndroidJavaObject currentActivity;
        public static AndroidJavaObject vibrator;
#endif

        private const string vibrate = "vibrate";

        public static void Vibrate()
        {
            if (isAndroid())
                vibrator.Call(vibrate);
            else
                Handheld.Vibrate();
        }

        public static void Vibrate(long milliseconds)
        {
            milliseconds = milliseconds < 50 ? 50 : milliseconds;

            if (isAndroid())
                vibrator.Call(vibrate, milliseconds);
            else
                Handheld.Vibrate();
        }

        public static bool HasVibrator()
        {
            return isAndroid();
        }

        public static void Cancel()
        {
            if (isAndroid())
                vibrator.Call(vibrate);
        }

        private static bool isAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
	    return true;
#else
            return false;
#endif
        }
    }
}