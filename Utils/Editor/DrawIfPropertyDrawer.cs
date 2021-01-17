using AnilTools.AnilEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    bool HasEvent;

    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && drawIf.disablingType == DisablingType.DontDraw)
            return 0f;

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;
        // Replace propertyname to the value from the parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        switch (drawIf.comparisonType)
        {
            case ComparisonType.Equals:
                switch (comparedField.type)
                {
                    case "float":
                        return comparedField.floatValue.Equals(drawIf.comparedValue);
                    case "int":
                        return comparedField.intValue.Equals(drawIf.comparedValue);
                    case "bool":
                        return comparedField.boolValue.Equals(drawIf.comparedValue);
                    case "Enum":
                        return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
                    default:
                        return true;
                }
            case ComparisonType.NotEqual:
                switch (comparedField.type)
                {
                    case "float":
                        return !comparedField.floatValue.Equals(drawIf.comparedValue);
                    case "int":
                        return !comparedField.intValue.Equals(drawIf.comparedValue);
                    case "bool":
                        return !comparedField.boolValue.Equals(drawIf.comparedValue);
                    case "Enum":
                        return !comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
                    default:
                        return true;
                }
            case ComparisonType.GreaterThan:
                switch (comparedField.type)
                { 
                    case "float":
                        return comparedField.floatValue > (float)drawIf.comparedValue;
                    case "int":
                        return comparedField.intValue > (int)drawIf.comparedValue;
                    default:
                        return true;
                }
            case ComparisonType.SmallerThan:
                switch (comparedField.type)
                {
                    case "Float":
                        return comparedField.floatValue < (float)drawIf.comparedValue;
                    case "int":
                        return comparedField.intValue < (int)drawIf.comparedValue;
                    default:
                        return true;
                }
            case ComparisonType.IsNull:

                return drawIf.comparedValue == null;
            
            default:
                return true;
        }
        
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property,true);
        }
        else if (drawIf.disablingType == DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = true;
        }
    }

}
