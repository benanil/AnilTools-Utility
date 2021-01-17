

using System;
using System.Collections.Generic;

namespace AnilTools.Update
{
    public struct WaitUntilTask : ITickable
    {
        private readonly Queue<WaitTaskData> tasks;
        private WaitTaskData currentTask;

        private readonly int calledInstanceId;


        public WaitUntilTask(Func<bool> endCondition, Action endAction , int calledInstanceId = 0)
        {
            this.calledInstanceId = calledInstanceId;
            tasks = new Queue<WaitTaskData>();
            currentTask = new WaitTaskData(endCondition,endAction);
        }

        public void Tick()
        {
            if (currentTask.endCondition.Invoke()){
                currentTask.EndAction.Invoke();
                currentTask = tasks.Dequeue();
                if (tasks.Count == 0){
                    AnilUpdate.Tasks.Remove(this);
                }
            }
        }

        public void Join(Func<bool> endCondition, Action endAction)
        {
            tasks.Enqueue(new WaitTaskData(endCondition, endAction));
        }
        
        public int InstanceId() => calledInstanceId;

        private readonly struct WaitTaskData
        { 
            public readonly Func<bool> endCondition;
            public readonly Action EndAction;

            internal WaitTaskData(Func<bool> endCondition, Action endAction)
            {
                this.endCondition = endCondition;
                EndAction = endAction;
            }
        }

    }
}
