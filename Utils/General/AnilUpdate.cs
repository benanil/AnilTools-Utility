using AnilTools.Move;
using AnilTools.Update;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnilTools
{
    public class AnilUpdate : MonoBehaviour
    {
        private static AnilUpdate _instance;

        public static readonly List<ITickable> Tasks = new List<ITickable>();

        private void Update()
        {
            for (short i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].Tick();
            }
        }

        public static void Register(ITickable task)
        {
            // be assure instance exist
            Checkinstance();
            var ableTask = Tasks.Find(x => x.InstanceId() == task.InstanceId() && x.InstanceId() != 0);

            if (ableTask != null)
            {
                switch (ableTask){
                    case MoveTask move:          
                    move.Join(((MoveTask)task).CurrentData);break;
                    case DirectionTask  drecTask:
                    drecTask.Join(((DirectionTask)task).CurrentData);break;
                }
            }
            else
            {
                Tasks.Add(task);
            }
        }

        public static void Checkinstance()
        {
            if (_instance == null)
            {
                var obj = new GameObject("Anil Update");
                _instance = obj.AddComponent<AnilUpdate>();
            }
        }
    }

    [CustomEditor(typeof(AnilUpdate))]
    public class AnilUpdateEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUIStyle gUIStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal =
                {
                    textColor = Color.white ,
                }
            };

            EditorGUILayout.LabelField("Debug", new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                fontSize = 20,
                margin = new RectOffset(40, 40, 60, 70),
                normal =
                {
                    textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black,
                }
            });

            EditorGUILayout.Space();

            for (int i = 0; i < AnilUpdate.Tasks.Count; i++)
            {
                if (AnilUpdate.Tasks[i] is MoveTask moveTask)
                {
                    EditorGUILayout.LabelField("Move task", gUIStyle);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Rotated " + moveTask.RotationReached.ToString());
                    EditorGUILayout.TextField("Name " + UnityShortCuts.FindObjectFromInstanceID(moveTask.InstanceId()).name);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Moved " + moveTask.PositionReached.ToString());
                    EditorGUILayout.EndHorizontal();
                }

                if (AnilUpdate.Tasks[i] is UpdateTask updateTask)
                {
                    EditorGUILayout.LabelField("Update task", gUIStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (updateTask.InstanceId() != 0)
                        EditorGUILayout.LabelField("Called Object: " + UnityShortCuts.FindObjectFromInstanceID(updateTask.InstanceId()).name);
                    EditorGUILayout.LabelField("Current queue" + updateTask.dataQueue.Count.ToString());
                    EditorGUILayout.EndHorizontal();
                }

                if (AnilUpdate.Tasks[i] is Timer timer)
                {
                    EditorGUILayout.LabelField("Timer", gUIStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (timer.InstanceId() != 0)
                        EditorGUILayout.LabelField(UnityShortCuts.FindObjectFromInstanceID(timer.InstanceId()).name);
                    EditorGUILayout.LabelField("Remaining time");
                    EditorGUILayout.LabelField(timer.time.ToString());
                    EditorGUILayout.EndHorizontal();
                }

                if (AnilUpdate.Tasks[i] is WaitUntilTask waitTask)
                {
                    EditorGUILayout.LabelField("wait task", gUIStyle);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Status: not finished");
                    if (waitTask.InstanceId() != 0)
                        EditorGUILayout.LabelField("Called Object: " + UnityShortCuts.FindObjectFromInstanceID(waitTask.InstanceId()).name);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
