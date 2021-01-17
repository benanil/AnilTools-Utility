using System;
using UnityEngine;

namespace AnilTools
{
    public struct Timer : ITickable
    {
        private readonly float DefaultTime;
        private event Action EndAction;
        // editor için public
        public float time;

        private readonly int calledObjectId;

        public Timer(float defaultTime, int calledObjectId = 0)
        {
            DefaultTime = defaultTime;
            this.calledObjectId = calledObjectId;
            this.time = defaultTime;
            EndAction = null;
            AnilUpdate.Register(this);
        }
        
        public Timer(float defaultTime, Action endAction , int calledObjectId = 0)
        {
            DefaultTime = defaultTime;
            this.calledObjectId = calledObjectId;
            this.time = defaultTime;
            this.EndAction = endAction;
            AnilUpdate.Register(this);
        }

        public void Tick()
        {
            time -= Time.deltaTime;
            if (TimeHasCome())
            {
                if (EndAction != null)
                {
                    EndAction.Invoke();
                    AnilUpdate.Tasks.Remove(this);
                }
                time = DefaultTime;
            }
        }

        public bool TimeHasCome()
        {
            return time < 0;
        }

        public int InstanceId() => calledObjectId;
    }
}
