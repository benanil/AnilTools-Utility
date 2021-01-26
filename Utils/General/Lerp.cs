

using AnilTools.Update;
using System;
using UnityEngine;

namespace AnilTools.Lerp
{
    public static class Lerp
    {
        // Animator
        public static UpdateTask LerpAnim(this Animator animator, int hashset, float value, float speed, Action then = null , float stopValue = 0.1f)
        {
            var task = new UpdateTask(() => animator.SetFloatLerp(hashset, value, speed), () => animator.Difrance(hashset, value) > stopValue, then, animator.GetInstanceID());
            AnilUpdate.Register(task);
            return task;
        }

        // ses , canvas , sprite , material , uı

    }
}