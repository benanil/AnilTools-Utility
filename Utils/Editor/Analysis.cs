
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AnilTools.AnilEditor
{
    public class Analysis : EditorWindow
    {
        private static List<Problem> problems = new List<Problem>();
        
        [MenuItem("Anil/Analysis")]
        public static void StartWindow()
        {
            var window = GetWindow<Analysis>();
            window.titleContent = new GUIContent("Analysis");
        }

        public static void AddProblem(string name)
        {
            if (!problems.Any(x => x.name == name)) {
                problems.Add(new Problem(name,0));
            }
            else{
                Problem problem = problems.Find(x => x.name == name);

                if (problem != null) {
                    problem.count++;
                }
            }
        }

        private void OnGUI()
        {
            for (int i = 0; i < problems.Count; i++){
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(problems[i].name);
                EditorGUILayout.LabelField(problems[i].count.ToString());
                EditorGUILayout.EndHorizontal();
            }
        }

        public struct Problem
        {
            public readonly string name;
            public int count;

            public Problem(string name, int count)
            {
                this.name = name;
                this.count = count;
            }

            public static bool operator ==(Problem a, Problem b)
            {
                return a.name.Equals(b.name);
            }
            public static bool operator !=(Problem a, Problem b)
            {
                return !a.name.Equals(b.name);
            }
        }
    }
}