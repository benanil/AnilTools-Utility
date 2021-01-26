

using UnityEngine;

namespace AnilTools
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
    }

    public static class Helper
    {
        private const string ClassName = "android.widget.Toast";
        private const string ClassName1 = "java.lang.String";
        private const string MethodName = "getApplicationContext";
        private const string MethodName1 = "makeText";

        /// <summary>
        /// singleton
        /// </summary>
        public static T instance<T>(this T a) where T : MonoBehaviour
        {
            if (a == null)
            {
                a = GameObject.FindObjectOfType<T>();
            }

            return a;
        }

        // ekranın orta alt kısmında yazı gösterir
        public static void ShowToast(string text)
        {
#if UNITY_ANDROID
            if (Application.isMobilePlatform)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                activity.Call("runOnUiThread", new AndroidJavaRunnable(
                  () =>
                  {
                      AndroidJavaClass Toast = new AndroidJavaClass(ClassName);
                      AndroidJavaObject javaString = new AndroidJavaObject(ClassName1, text);
                      AndroidJavaObject context = activity.Call<AndroidJavaObject>(MethodName);
                      AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>(MethodName1, context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
                      toast.Call("show");
                  }
                  ));
            }
#endif
        }

    }
    // Color Helper
    public enum HelpColor
    {
        white, blue, yellow, green, orange, red, pink, cyan
    }
}
