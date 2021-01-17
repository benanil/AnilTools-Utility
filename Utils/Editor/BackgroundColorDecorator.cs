using UnityEngine;
using UnityEditor;

namespace AnilTools.AnilEditor
{
    [CustomPropertyDrawer(typeof(BackgroundColorAttribute))]
    public class BackgroundColorDecorator : DecoratorDrawer
    {
        BackgroundColorAttribute attr { get { return (BackgroundColorAttribute)attribute; } }
        public override float GetHeight() { return 0; }

        public override void OnGUI(Rect position)
        {
            GUI.backgroundColor = attr.color;
        }
    }
}