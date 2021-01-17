using UnityEngine;
using UnityEditor;

/// <summary>
/// This class contain custom drawer for ReadOnly attribute.
/// </summary>
[CustomPropertyDrawer(typeof(DontDrawAttribute))]
public class DontDrawer : PropertyDrawer
{

    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

    }

}