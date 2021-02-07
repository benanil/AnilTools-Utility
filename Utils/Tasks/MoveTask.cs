using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnilTools.Move
{
    // Todo : add Ease. complated

    public class MoveTask : ITickable
    {
        internal const byte DefaultSpeed = 3;
        internal const byte DefaultRotationSpeed = 2;

        public readonly Transform from;

        private readonly Queue<MoveData> queue;
        private readonly List<Action> EndAction = new List<Action>();
        internal MoveData CurrentData;
        private readonly MoveType moveType = MoveType.towards;

        /// <summary>
        /// <param><b>Move Speed: </b></param>
        /// <para>movetype lerp yada towards değilse nokraya varacağı süre</para>
        /// </summary>
        public float MoveSpeed = DefaultSpeed;
        public float RotateSpeed = DefaultRotationSpeed;

        // editörde gözükmesi için public yapıldı
        public bool RotationReached
        {
            get
            {
                if (Quaternion.Angle(from.rotation, Quaternion.Euler(CurrentData.eulerAngles)) < Mathf.Epsilon || CurrentData.eulerAngles == Vector3.zero)
                {
                    from.eulerAngles = CurrentData.eulerAngles; 
                    return true;
                }
                return false;
            }
        }

        public bool PositionReached => from.Distance(CurrentData.pos) < Mathf.Epsilon || CurrentData.pos == Vector3.zero;

        /// <summary> it will add position to transform</summary>
        private readonly bool IsPlus = false;
        private readonly bool IsLocal = false;

        public AnimationCurve animationCurve;
        private float timer;

        public void Tick()
        {
            if (!PositionReached){

                if (moveType == MoveType.lerp)
                {
                    if (IsLocal) from.localPosition  = Vector3.Lerp(from.localPosition, CurrentData.pos, MoveSpeed * Time.deltaTime);
                    else         from.position       = Vector3.Lerp(from.position, CurrentData.pos, MoveSpeed * Time.deltaTime);
                }
                else if (moveType == MoveType.towards)
                {
                    if (IsLocal) from.localPosition  = Vector3.MoveTowards(from.localPosition, CurrentData.pos, MoveSpeed * Time.deltaTime);
                    else from.position               = Vector3.MoveTowards(from.position, CurrentData.pos, MoveSpeed * Time.deltaTime);
                }
                else if (moveType == MoveType.Curve)
                {
                    if (IsLocal) from.localPosition  = Vector3.Lerp(from.localPosition, CurrentData.pos, animationCurve.Evaluate(timer / MoveSpeed));
                    else         from.position       = Vector3.Lerp(from.position, CurrentData.pos     , animationCurve.Evaluate(timer / MoveSpeed));
                    
                    timer += Time.deltaTime;
                }
            }

            if (!RotationReached){
                from.localRotation = Quaternion.Slerp(from.rotation, Quaternion.Euler(CurrentData.eulerAngles), MoveSpeed * RotateSpeed * Time.deltaTime);
            }

            if (PositionReached && RotationReached)
            {
                if (queue.Count == 0)
                {
                    EndAction.ForEach(x => x?.Invoke());
                    AnilUpdate.Tasks.Remove(this);
                    return;
                }
                CurrentData = queue.Dequeue();
                CurrentData = FillCurrentData(CurrentData.pos, CurrentData.eulerAngles);
            }
        }

        public void AddFinishEvent(Action action)
        {
            EndAction.Add(action);
        }

        public MoveTask Join(Vector3 Position)
        {
            queue.Enqueue(new MoveData(Position, Vector3.zero));
            return this;
        }

        public MoveTask JoinRotation(Vector3 Rotation)
        {
            queue.Enqueue(new MoveData(Vector3.zero, Rotation));
            return this;
        }

        public MoveTask Join(MoveData moveData)
        {
            queue.Enqueue(moveData);
            return this;
        }

        public MoveTask Join(Transform transform)
        {
            queue.Enqueue(new MoveData(transform));
            return this;
        }

        private MoveData FillCurrentData(Vector3 position, Vector3 eulerAngles)
        {
            if (IsPlus){
                if (IsLocal){
                    position += from.localPosition;
                    eulerAngles += from.localEulerAngles;
                }
                else{
                    position += from.position;
                    eulerAngles += from.eulerAngles;
                }
            }

            return new MoveData(position, eulerAngles);
        }

        public MoveTask(Transform from, Transform to, float speed = DefaultSpeed, float rotateSpeed = DefaultRotationSpeed
                        ,MoveType moveType = MoveType.towards, bool isPlus = false , bool isLocal = false , Action endAction = null)
        {

            queue = new Queue<MoveData>();
            this.from = from;

            CheckAction(endAction);

            IsPlus = isPlus;

            CurrentData = FillCurrentData(to.position, to.eulerAngles);

            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.MoveSpeed = speed;
            this.RotateSpeed = rotateSpeed;
        }

        public MoveTask(Transform from, Vector3 to, AnimationCurve animationCurve , float speed = DefaultSpeed , float rotateSpeed = DefaultRotationSpeed
                        , MoveType moveType = MoveType.towards, bool isPlus = false, bool isLocal = false, Action endAction = null)
        {
            queue = new Queue<MoveData>();
            this.from = from;
            IsPlus = isPlus;

            CheckAction(endAction);

            CurrentData = FillCurrentData(to, Vector3.zero);

            this.animationCurve = animationCurve;

            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.MoveSpeed = speed;
            this.RotateSpeed = rotateSpeed;
        }

        public MoveTask(Transform from, Vector3 pos , Vector3 rot , float speed = DefaultSpeed, float rotateSpeed = DefaultRotationSpeed
                        , MoveType moveType = MoveType.towards, bool isPlus = false, bool isLocal = false, Action endAction = null)
        {
            queue = new Queue<MoveData>();
            this.from = from;
            IsPlus = isPlus;


            CheckAction(endAction);
            CurrentData = FillCurrentData(pos, rot);

            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.MoveSpeed = speed;
            this.RotateSpeed = rotateSpeed;
        }

        public MoveTask(Transform from, Vector3 to, float speed = DefaultSpeed, MoveType moveType = MoveType.towards, 
                        bool isPlus = false, bool isLocal = false , Action endAction = null)
        {
            queue = new Queue<MoveData>();
            this.from = from;

            CheckAction(endAction);

            CurrentData = new MoveData(to, Vector3.zero);
            
            IsPlus = isPlus;
            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.MoveSpeed = speed;
        }

        public MoveTask(Vector3 euler, Transform from, float rotateSpeed = DefaultRotationSpeed
                       , bool isPlus = false, bool isLocal = false , Action endAction = null)
        {
            queue = new Queue<MoveData>();
            this.from = from;
            
            IsPlus = isPlus;

            CurrentData = FillCurrentData(Vector3.zero, euler);
            CheckAction(endAction);

            this.IsLocal = isLocal;
            this.RotateSpeed = rotateSpeed;
        }

        private void CheckAction(Action action)
        {
            if (action != null)
            {
                EndAction.Add(action);
            }
        }

        public int InstanceId()
        {
            return from.GetInstanceID();
        }

        public class MoveData
        {
            public Vector3 pos;
            public Vector3 eulerAngles;

            public MoveData(Transform transform)
            {
                pos = transform.position;
                eulerAngles = transform.eulerAngles;
            }

            public MoveData(Vector3 pos, Vector3 rot)
            {
                this.pos = pos;
                this.eulerAngles = rot;
            }
        }

    }
}

