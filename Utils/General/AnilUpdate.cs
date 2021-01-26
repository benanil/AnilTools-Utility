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
        public static readonly List<ITickable> FixedTasks = new List<ITickable>();

        private void Update()
        {
            for (short i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].Tick();
            }
        }

        private void FixedUpdate()
        {
            for (short i = 0; i < FixedTasks.Count; i++)
            {
                FixedTasks[i].Tick();
            }
        }

        public static void Remove(ITickable tickable)
        {
            if (Tasks.Contains(tickable))
            {
                Tasks.Remove(tickable);
            }
            else if (FixedTasks.Contains(tickable))
            {
                FixedTasks.Remove(tickable);
            }
            else Debug2.LogError("Itickable kaldırılamadı");
        }

        public static void Register(ITickable task, UpdateType updateType = UpdateType.normal)
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
                    case UpdateTask updateTask:
                    Debug2.Log("update task exist it will add in queue");
                    updateTask.Join(((UpdateTask)task).currentData);break;
                }
            }
            else
            {
                if (updateType == UpdateType.normal)         Tasks.Add(task);
                else if (updateType == UpdateType.fixedTime) FixedTasks.Add(task);
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
        readonly List<MoveTask> moveTasks = new List<MoveTask>();
        readonly List<UpdateTask> updateTasks = new List<UpdateTask>();
        readonly List<Timer> timers = new List<Timer>();
        readonly List<WaitUntilTask> waitUntilTasks = new List<WaitUntilTask>();

        public override void OnInspectorGUI()
        {
            moveTasks.Clear();
            updateTasks.Clear();
            timers.Clear(); 
            waitUntilTasks.Clear();

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
                switch (AnilUpdate.Tasks[i])
                {
                    case MoveTask move: moveTasks.Add(move); break;
                    case UpdateTask update: updateTasks.Add(update); break;
                    case Timer timerTask: timers.Add(timerTask); break;
                    case WaitUntilTask untilTask: waitUntilTasks.Add(untilTask); break;
                    default: break;
                }
            }

            EditorGUILayout.LabelField("Update Tasks", gUIStyle);
            for (int i = 0; i < updateTasks.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (updateTasks[i].InstanceId() != 0)
                    EditorGUILayout.LabelField("Sender: " + UnityShortCuts.FindObjectFromInstanceID(updateTasks[i].InstanceId()).name);
                EditorGUILayout.LabelField("Queue" + updateTasks[i].dataQueue.Count.ToString());
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField("Timers", gUIStyle);
            for (int i = 0; i < timers.Count; i++)
            {
                var timer = timers[i];
                EditorGUILayout.BeginHorizontal();
                if (!string.IsNullOrEmpty(timer.name))
                    EditorGUILayout.LabelField("name: " + timer.name);
                if (timer.InstanceId() != 0)
                    EditorGUILayout.LabelField("Sender: " + UnityShortCuts.FindObjectFromInstanceID(timer.InstanceId()).name);
                EditorGUILayout.LabelField("Remaining time: " + timer.time.ToString());
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField("Wait Tasks", gUIStyle);
            for (int i = 0; i < waitUntilTasks.Count; i++)
            {
                var waitTask = waitUntilTasks[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Status: not finished");
                if (waitTask.InstanceId() != 0)
                    EditorGUILayout.LabelField("Sender: " + UnityShortCuts.FindObjectFromInstanceID(waitTask.InstanceId()).name);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField("Move Tasks", gUIStyle);
            for (int i = 0; i < moveTasks.Count; i++)
            {
                var moveTask = moveTasks[i];
                EditorGUILayout.BeginHorizontal();
                if (moveTask.InstanceId() != 0)
                    EditorGUILayout.LabelField("Sender: " + UnityShortCuts.FindObjectFromInstanceID(moveTask.InstanceId()).name);
                EditorGUILayout.LabelField("Rotation Finished: " + moveTask.RotationReached.ToString());
                EditorGUILayout.LabelField("Position Finished: " + moveTask.PositionReached.ToString());
                EditorGUILayout.EndHorizontal();
            }

        }
    }

}
