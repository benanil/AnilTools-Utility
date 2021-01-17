using UnityEngine;
using UnityEngine.UI;

namespace AnilTools
{ 
    public class SystemInfoDisplayer : MonoBehaviour
    {
        public static string Info
        {
            get
            { 
                var text = $"{$"{SystemInfo.graphicsDeviceType} "}" +
                           $"{$"mem {SystemInfo.systemMemorySize} "}" +
                           $"{$"gpu mem: {SystemInfo.graphicsMemorySize}"}";

                #if UNITY_EDITOR
                text += " cpu " + SystemInfo.processorType.Remove(0, 17);
                #endif
                #if !UNITY_EDITOR
                text += $"{SystemInfo.deviceModel} {SystemInfo.deviceName}";
                text += " cpu " + SystemInfo.processorType;
                #endif
                text += $"<color=grey> game ver: {Application.version}</color>";
            
                return text;
            }
        }

        private void Start()
        {
            GetComponent<Text>().text = Info;
        }

    }
}
