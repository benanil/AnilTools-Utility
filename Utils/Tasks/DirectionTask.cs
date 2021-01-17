using System;
using System.Collections.Generic;
using UnityEngine;
using UrFairy;

namespace AnilTools.Move
{

    public enum MoveDirection
    {
        x, y, z
    }

    public struct DirectionTask : ITickable
    {
        private const float Tolerance = .002f;

        public readonly Transform from;
        private Queue<DirectionData> queue;
        public DirectionData CurrentData;
        public float speed;
        public readonly bool isPlus;
        public readonly bool isLocal;
        public event Action EndEvent;

        public DirectionTask(Transform from, MoveDirection moveDirection, float pos, float speed = 5, bool isPlus = false, bool isLocal = false)
        {
            EndEvent = null;
            this.isLocal = isLocal;
            this.isPlus = isPlus;
            queue = new Queue<DirectionData>();
            this.from = from;
            this.CurrentData = new DirectionData(moveDirection, pos);
            this.speed = speed;
        }


        public DirectionTask Join(float pos, MoveDirection moveDirection)
        {
            queue.Enqueue(new DirectionData(moveDirection , pos));
            return this;
        }

        public DirectionTask Join(DirectionData directionData)
        {
            queue.Enqueue(directionData);
            return this;
        }

        public DirectionTask AddSpeed(float value)
        {
            speed += value;
            return this;
        }

        public void Tick()
        {
            switch (CurrentData.direction){
                case MoveDirection.x: from.position = from.position.XLerp(CurrentData.pos, speed); break;
                case MoveDirection.y: from.position = from.position.YLerp(CurrentData.pos, speed); break;
                case MoveDirection.z: from.position = from.position.ZLerp(CurrentData.pos, speed); break;
            }

            if (Distance() < Tolerance)
            {
                if (queue.Count == 0) {
                    AnilUpdate.Tasks.Remove(this);
                    EndEvent?.Invoke();
                    return;
                }
                CurrentData = queue.Dequeue();
                if (isPlus)
                {
                    CurrentData.Add(CurrentValue());
                }
            }
        }

        private float Distance()
        {
            switch (CurrentData.direction){
                case MoveDirection.x: return from.position.x.Difrance(CurrentData.pos);
                case MoveDirection.y: return from.position.y.Difrance(CurrentData.pos);
                case MoveDirection.z: return from.position.z.Difrance(CurrentData.pos);
                default: return 0;
            }
        }

        private float CurrentValue()
        {
            switch (CurrentData.direction){
                case MoveDirection.x: return from.position.x;
                case MoveDirection.y: return from.position.y;
                case MoveDirection.z: return from.position.z;
                default: return 0;
            }
        }

        public int InstanceId()
        {
            return from.GetInstanceID();
        }

        public struct DirectionData
        {
            public readonly MoveDirection direction;
            public float pos;

            internal void Add(float value)
            {
                pos += value;
            }

            public DirectionData(MoveDirection direction, float pos)
            {
                this.direction = direction;
                this.pos = pos;
            }
        }
    }

}
