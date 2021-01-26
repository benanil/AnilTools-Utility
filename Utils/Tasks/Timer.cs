using System;
using UnityEngine;

namespace AnilTools
{
    [Serializable]
    public class Timer : ITickable, IDisposable
    {
        public float DefaultTime;

        private event Action EndAction;
        // editor için public
        [SerializeField,ReadOnly]
        private float _time;
        public float time
        {
            get{
                return _time;
            }
            set{
                _time = value < 0 ? 0 : value;
            }
        }

        public readonly string name;
        
        [NonSerialized] public int calledObjectId;
        [NonSerialized] public bool OtoReset;
        [ReadOnly] private bool update;

        public Timer(float defaultTime, int calledObjectId = 0, bool OtoReset = false, Action endAction = null, string name = "")
        {
            DefaultTime = defaultTime;
            this.calledObjectId = calledObjectId;
            time = defaultTime;
            EndAction = endAction;
            this.OtoReset = OtoReset;
            this.name = name;
            update = true;
            AnilUpdate.Register(this);
        }
        
        public Timer(float defaultTime, Action endAction , int calledObjectId = 0, bool OtoReset = false , string name = "")
        {
            DefaultTime = defaultTime;
            this.calledObjectId = calledObjectId;
            time = defaultTime;
            EndAction = endAction;
            this.OtoReset = OtoReset;
            this.name = name;
            update = true;
            AnilUpdate.Register(this);
        }

        public void Tick()
        {
            if(update)
            time -= Time.deltaTime;
            
            if (TimeHasCome())
            {
                if (EndAction != null){
                    if (OtoReset){
                        EndAction.Invoke();
                        Reset();
                        return;
                    }
                    EndAction.Invoke();
                    Dispose();
                }
            }
        }

        // methods
        public void Dispose() => AnilUpdate.Tasks.Remove(this);
        public void Reset()   => time = DefaultTime;
        public void Stop()    => update = false;
        public void Start()   => update = true;
        
        // funcs
        public bool TimeHasCome() => time <= 0.01f;
        public int InstanceId()   => calledObjectId;

    }
}
