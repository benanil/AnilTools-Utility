using UnityEditor;
using UnityEngine;

namespace AnilTools.AnilEditor
{
    [CustomPropertyDrawer(typeof(ScriptableAttribute))]
    public class ScriptableDrawer : PropertyDrawer
    {
        bool showProperty = false;
        float DrawerHeight = 0;
        string button = "-";

        ScriptableAttribute scriptable;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var e = Editor.CreateEditor(property.objectReferenceValue);
            var indent = EditorGUI.indentLevel;
            Rect temp = new Rect(position.x - 12, position.y, 16, 16);

            scriptable = attribute as ScriptableAttribute;
            if (scriptable.Openable)
            {
                if (GUI.Button(temp, button))
                {

                    if (showProperty)
                    {
                        showProperty = false;
                        button = "-";
                    }
                    else
                    {
                        showProperty = true;
                        button = "|";
                    }
                }
            }
            else
            {
                showProperty = true;
            }

            DrawerHeight = 0;
            position.height = 16;

            EditorGUI.PropertyField(position, property);
            position.y += 20;
            if (!showProperty) return;
            if (e != null)
            {
                position.x += 20;
                position.width -= 40;
                var so = e.serializedObject;
                so.Update();
                var prop = so.GetIterator();
                //Debug.Log(" prop.hasVisibleChildren " + prop.hasVisibleChildren);
                prop.NextVisible(true);
                int depthChilden = 0;
                bool showChilden = false;
                while (prop.NextVisible(true))
                {
                    if (prop.depth == 0) { showChilden = false; depthChilden = 0; }
                    if (showChilden && prop.depth > depthChilden)
                    {
                        continue;
                    }
                    position.height = 16;
                    EditorGUI.indentLevel = indent + prop.depth;
                    if (EditorGUI.PropertyField(position, prop))
                    {
                        showChilden = false;
                    }
                    else
                    {
                        showChilden = true;
                        depthChilden = prop.depth;
                    }
                    position.y += 20;
                    SetDrawerHeight(20);
                }

                if (GUI.changed)
                {
                    so.ApplyModifiedProperties();
                }
            }

        }

        void SetDrawerHeight(float height)
        {
            DrawerHeight += height;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            var e = Editor.CreateEditor(property.objectReferenceValue);
            if (e != null)
            {
                var so = e.serializedObject;
                var prop = so.GetIterator();
                prop.NextVisible(true);
                while (prop.NextVisible(true)) height += 20;
            }
            return height;
        }
    }
}