using UnityEditor;
using UnityEngine;

namespace AnilTools.AnilEditor
{
    public class HeaderColorDrawer : PropertyDrawer
    {
        HeaderAttribute headerAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            headerAttribute = attribute as HeaderAttribute;

            GUIStyle style = new GUIStyle();

            style.normal.textColor = Color.white;

            EditorGUILayout.LabelField(headerAttribute.header, style);
            base.OnGUI(position, property, label);
        }
    }
}