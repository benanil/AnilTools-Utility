using System;
using System.Collections.Generic;
namespace AnilTools.Update
{
    public struct UpdateTask : ITickable
    {
        public readonly Queue<ActionData> dataQueue;
        private event Action EndAction;
        public ActionData currentData;

        //for debug
        private readonly int CalledInstanceId;

        public UpdateTask(Action updateAction, Func<bool> endCondition, Action endAction = null , int calledInstanceId = 0)
        {
            switch (endAction){
                case null: EndAction = () => { };  break;
                default:   EndAction = endAction;  break;
            }
            
            dataQueue = new Queue<ActionData>();
            currentData = new ActionData(updateAction,endCondition);
            this.CalledInstanceId = calledInstanceId;
        }

        public UpdateTask Join(ActionData actionData)
        {
            dataQueue.Enqueue(actionData);
            return this;
        }

        public UpdateTask Join(Action action, Func<bool> endCondition)
        {
            dataQueue.Enqueue(new ActionData(action,endCondition));
            return this;
        }

        public void Tick()
        {
            currentData.Invoke();

            if (!currentData.CheckEnd())
            {
                if (dataQueue.Count == 0)
                {
                    EndAction?.Invoke();
                    AnilUpdate.Tasks.Remove(this);
                    return;
                }
                currentData = dataQueue.Dequeue();
            }
        }

        public int InstanceId() => CalledInstanceId;

        public struct ActionData
        {
            public readonly Action updateAction;
            public readonly Func<bool> endCondition;

            internal void Invoke()
            {
                updateAction.Invoke();
            }

            internal bool CheckEnd()
            {
                return endCondition.Invoke();
            }

            public ActionData(Action updateAction, Func<bool> endCondition)
            {
                this.updateAction = updateAction;
                this.endCondition = endCondition;
            }
        }

    }
}
