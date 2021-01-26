using System;

namespace AnilTools.Update
{
    public static class RegisterUpdate
    {
        /// <summary>
        /// this allows you update action, <b>you can recive input</b>
        /// </summary>
        /// <param name="action"><param>will be updated action</param>
        /// <param name="endCnd"><param>do while this condition true</param>
        /// <param name="then">then do</param>
        public static UpdateTask UpdateWhile(Action action, Func<bool> endCnd, Action then = null, UnityEngine.Object calledInstance = null, UpdateType updateType = UpdateType.normal)
        {
#if UNITY_EDITOR
            int id = calledInstance ? calledInstance.GetInstanceID() : 0;
            var task = new UpdateTask(action, endCnd, then, id);
#else
            var task = new UpdateTask(action, endCondition, then);
#endif
            AnilUpdate.Register(task, updateType);
            return task;
        }

        public static UpdateTask UpdateWhile(this UnityEngine.Object sender , Action action, Func<bool> endCondition, Action then = null, UpdateType updateType = UpdateType.normal)
        {
            var task = new UpdateTask(action, endCondition, then, sender ? sender.GetInstanceID() : 0);
            AnilUpdate.Register(task, updateType);
            return task;
        }

        public static UpdateTaskGeneric<T> UpdateWhile<T>(this T sender, Action<T> action, Func<T,bool> endCondition, Action then = null, UpdateType updateType = UpdateType.normal)
        {
            var task = new UpdateTaskGeneric<T>(action, endCondition, sender, then);
            AnilUpdate.Register(task, updateType);
            return task;
        }

        /// <summary>
        /// wait while condition false then action
        /// </summary>
        public static WaitUntilTask WaitUntil(Func<bool> endCondition, Action then, UnityEngine.Object calledInstance = null)
        {
#if UNITY_EDITOR
            int id = calledInstance ? calledInstance.GetInstanceID() : 0;
            var task = new WaitUntilTask(endCondition, then, id);
#else
            var task = new WaitUntilTask(endCondition, then);
#endif
            AnilUpdate.Register(task, UpdateType.normal);
            return task;
        }
    }
}