using UnityEngine;

namespace AnilTools.Move
{
    public static partial class NormalDirection
    {
        public static DirectionTask MoveX(this Transform from, float pos, float speed = 5, bool isLocal = true, bool isPlus = true)
        {
            var task = new DirectionTask(from, MoveDirection.x, pos, speed, isPlus, isLocal);
            AnilUpdate.Register(task);
            return task;
        }

        public static Transform MoveY(this Transform from, float pos, float speed = 5, bool isLocal = true, bool isPlus = true)
        {
            var task = new DirectionTask(from, MoveDirection.y, pos, speed, isPlus, isLocal);
            AnilUpdate.Register(task);
            return from;
        }

        public static Transform MoveZ(this Transform from, float pos, float speed = 5, bool isLocal = true, bool isPlus = true)
        {
            var task = new DirectionTask(from, MoveDirection.z, pos, speed, isPlus, isLocal);
            AnilUpdate.Register(task);
            return from;
        }
    }
}
