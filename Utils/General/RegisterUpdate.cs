using System;

namespace AnilTools.Update
{
    public static class RegisterUpdate
    {
        /// <summary>
        /// this allows you update action, <b>you can recive input</b>
        /// </summary>
        /// <param name="action"><param>will be updated action</param>
        /// <param name="endCondition"><param>do while this condition true</param>
        /// <param name="then">then do</param>
        public static UpdateTask UpdateWhile(Action action, Func<bool> endCondition, Action then = null, UnityEngine.Object calledInstance = null)
        {
#if UNITY_EDITOR
            int id = calledInstance ? calledInstance.GetInstanceID() : 0;
            var task = new UpdateTask(action, endCondition, then, id);
#else
                var task = new UpdateTask(action, endCondition, then);
#endif
            AnilUpdate.Register(task);
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
            AnilUpdate.Register(task);
            return task;
        }
    }
}