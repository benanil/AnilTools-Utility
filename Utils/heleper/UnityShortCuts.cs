using UnityEditor;
using System.Reflection;
using System.Diagnostics;
using UnityEngine;
using System.Collections;

namespace AnilTools
{
    public class UnityShortCuts
    {
#if UNITY_EDITOR

        [MenuItem("Anil/ClearConsole %g")]
        public static void ClearConsole()
        {
            UnityEngine.Debug.ClearDeveloperConsole();
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [MenuItem("Anil/OpenXmlFolder %#o")]
        public static void OpenXmlFolder()
        {
            Process.Start(@"C:\Users\Administrator\AppData\LocalLow\Intence Desire Games\Forest Champ\Saves");
        }

        [MenuItem("Anil/OpenVideosFolder")]
        public static void OpenVideosFolder()
        {
            Process.Start(@"E:\CPY_SAVES\videos\bandicam");
        }

        [MenuItem("Anil/Collect Garbrage %#j")]
        public static void ClearGC()
        {
            Debug2.Log("garbrage collected: " + System.GC.CollectionCount(0).ToString(), Color.green);
            System.GC.Collect();
        }

        [MenuItem("Anil/Show Garbrage %#l")]
        public static void ShowGC()
        {
            Debug2.Log("Garbrage Collection is " + System.GC.CollectionCount(0).ToString() + " Collection");
        }

        public static Object FindObjectFromInstanceID(int iid)
        {
            return (Object)typeof(Object)
                    .GetMethod("FindObjectFromInstanceID", BindingFlags.NonPublic | BindingFlags.Static)
                    .Invoke(null, new object[] { iid });
        }
#endif

    }
}
