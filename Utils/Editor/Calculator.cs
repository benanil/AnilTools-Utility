
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnilTools.AnilEditor
{
    public class Calculator : EditorWindow
    {
        float half = 1;
        float Mt = 1;

        [MenuItem("Anil/Calculator")]
        public static void StartWindow()
        {
            var window = GetWindow<Calculator>();
            window.titleContent = new GUIContent("Calculator");
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(40);
            EditorGUILayout.LabelField(Mathf.Pow(half, 0.5f).ToString() + " mt");
            EditorGUILayout.Space(15);
            EditorGUILayout.LabelField(Mathf.Pow(Mt, 2).ToString() + " sqr");
        }

        private void OnEnable()
        {
            var halfField = new TextField("sqr To mt");
            halfField.RegisterValueChangedCallback(evt =>
            {
                float.TryParse(evt.newValue, out half);
            });
            rootVisualElement.Add(halfField);

            var MtField = new TextField("mt To sqr");
            MtField.RegisterValueChangedCallback(evt =>
            {
                float.TryParse(evt.newValue, out Mt);
            });
            rootVisualElement.Add(MtField);
        }

    }
}
