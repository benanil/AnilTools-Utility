using UnityEngine;

namespace AnilTools.Move
{
    public partial class  NormalMove
    {
        public static MoveTask RotateTransform(this Transform from, Vector3 to, float rotationSpeed = MoveTask.DefaultRotationSpeed, bool IsPlus = false)
        {
            var task = new MoveTask(to, from, rotateSpeed: rotationSpeed, isPlus: IsPlus);
            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask RotateTransform(this Transform from, Vector3[] to, float rotationSpeed = MoveTask.DefaultRotationSpeed, bool IsPlus = false)
        {
            var task = new MoveTask(to[0], from, rotateSpeed: rotationSpeed, isPlus: IsPlus);

            for (int i = 1; i < to.Length; i++)
            {
                task.JoinRotation(to[i]);
            }

            AnilUpdate.Register(task);
            return task;
        }
    }
}
