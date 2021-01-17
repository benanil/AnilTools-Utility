using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnilTools.Move
{
    // Todo : add Ease

    public class MoveTask : ITickable
    {
        internal const byte DefaultSpeed = 3;
        internal const byte DefaultRotationSpeed = 2;
        private const byte RotationTolerance = 1;
        private const float PositionTolerance = .01f;

        public readonly Transform from;

        private readonly Queue<MoveData> queue;
        private readonly MoveType moveType = MoveType.towards;
        private event Action EndAction;
        internal MoveData CurrentData;

        /// <summary>
        /// <param><b>Move Speed: </b></param>
        /// <para>movetype lerp yada towards değilse nokraya varacağı süre</para>
        /// </summary>
        public float MoveSpeed = DefaultSpeed;
        public float RotateSpeed = DefaultRotationSpeed;

        // editörde gözükmesi için public yapıldı
        public bool RotationReached => Quaternion.Angle(from.rotation, Quaternion.Euler(CurrentData.eulerAngles)) < RotationTolerance || CurrentData.eulerAngles == Vector3.zero;
        public bool PositionReached => from.Distance(CurrentData.pos) < PositionTolerance || CurrentData.pos == Vector3.zero;

        /// it will add position to transform
        private readonly bool IsPlus = false;
        private readonly bool IsLocal = false;

        public void Tick()
        {
            if (!PositionReached){
                switch (moveType)
                {
                    case MoveType.lerp:
                        if (IsLocal){
                            from.localPosition = Vector3.Lerp(from.localPosition, CurrentData.pos, MoveSpeed * Time.deltaTime);
                        }else{
                            from.position = Vector3.Lerp(from.position, CurrentData.pos, MoveSpeed * Time.deltaTime);
                        }
                        break;
                    case MoveType.towards:
                        if (IsLocal){
                            from.localPosition = Vector3.MoveTowards(from.localPosition, CurrentData.pos, MoveSpeed * Time.deltaTime);
                        }else{
                            from.position = Vector3.MoveTowards(from.position, CurrentData.pos, MoveSpeed * Time.deltaTime);
                        }
                        break;
                    default:
                        from.position = Vector3.MoveTowards(from.position, CurrentData.pos, MoveSpeed * Time.deltaTime);
                        break;
                }
            }

            if (!RotationReached){
                from.localRotation = Quaternion.Slerp(from.rotation, Quaternion.Euler(CurrentData.eulerAngles), MoveSpeed * RotateSpeed * Time.deltaTime);
            }

            if (PositionReached && RotationReached)
            {
                if (queue.Count == 0)
                {
                    EndAction?.Invoke();
                    AnilUpdate.Tasks.Remove(this);
                    Debug.Log("finish");
                    return;
                }
                CurrentData = queue.Dequeue();
                if (IsPlus)
                {
                    if (IsLocal){
                        CurrentData.pos += from.localPosition;
                        CurrentData.eulerAngles += from.localEulerAngles;
                    }
                    else{
                        CurrentData.pos += from.position;
                        CurrentData.eulerAngles += from.eulerAngles;
                    }
                }
            }
        }

        public void AddFinishEvent(Action action)
        {
            EndAction += action;
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

        public MoveTask(Transform from, Transform to, float speed = DefaultSpeed, float rotateSpeed = DefaultRotationSpeed
                        ,MoveType moveType = MoveType.towards, bool isPlus = false , bool isLocal = false , Action endAction = null)
        {
            queue = new Queue<MoveData>();

            EndAction = CheckAction(endAction);

            CurrentData = new MoveData(to.position, to.eulerAngles);
            IsPlus = isPlus;

            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.from = from;
            this.MoveSpeed = speed;
            this.RotateSpeed = rotateSpeed;
        }

        public MoveTask(Transform from, Vector3 pos , Vector3 rot , float speed = DefaultSpeed, float rotateSpeed = DefaultRotationSpeed
                        , MoveType moveType = MoveType.towards, bool isPlus = false, bool isLocal = false, Action endAction = null)
        {
            queue = new Queue<MoveData>();

            EndAction = CheckAction(endAction);

            CurrentData = new MoveData(pos, rot);
            IsPlus = isPlus;

            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.from = from;
            this.MoveSpeed = speed;
            this.RotateSpeed = rotateSpeed;
        }

        public MoveTask(Transform from, Vector3 to, float speed = DefaultSpeed, MoveType moveType = MoveType.towards, 
                        bool isPlus = false, bool isLocal = false , Action endAction = null)
        {
            queue = new Queue<MoveData>();

            EndAction = CheckAction(endAction);

            CurrentData = new MoveData(to, Vector3.zero);
            
            IsPlus = isPlus;
            this.IsLocal = isLocal;
            this.moveType = moveType;
            this.from = from;
            this.MoveSpeed = speed;
        }

        public MoveTask(Vector3 euler, Transform from, float rotateSpeed = DefaultRotationSpeed
                       , bool isPlus = false, bool isLocal = false , Action endAction = null)
        {
            queue = new Queue<MoveData>();
            
            CurrentData = new MoveData(Vector3.zero, euler);
            EndAction = CheckAction(endAction);

            this.IsLocal = isLocal;
            IsPlus = isPlus;
            this.from = from;
            this.RotateSpeed = rotateSpeed;
        }

        private static Action CheckAction(Action action)
        {
            switch (action){
                case null: return () => { };
                default:   return action;
            }
        }

        public int InstanceId()
        {
            return from.GetInstanceID();
        }

        public struct MoveData
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
