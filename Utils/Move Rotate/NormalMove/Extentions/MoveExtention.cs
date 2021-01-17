using System;
using System.Linq;
using UnityEngine;

namespace AnilTools.Move
{
    public static partial class NormalMove
    {   
        ///
        /// position rotation
        ///
        public static MoveTask Move(this Transform from, Vector3 to, float speed = MoveTask.DefaultSpeed, MoveType moveType = MoveType.towards,
                                    bool IsPlus = false, Action endAction = null)
        {
            var task = new MoveTask(from, to , speed , moveType , IsPlus , isLocal:false , endAction);

            
            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask MoveArray(this Transform from, Vector3[] to,bool reverse = false, float speed = MoveTask.DefaultSpeed, MoveType moveType = MoveType.towards,
                                         bool IsPlus = false ,bool isLocal = false, Action endAction = null)
        {

            var transforms = to.ToList();
            if (reverse){
                transforms.Reverse();
            }

            var task = new MoveTask(from, transforms[0] , speed , moveType , IsPlus , isLocal , endAction);

            for (int i = 1; i < to.Length; i++)
            {
                task.Join(transforms[i]);
            }

            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask MoveArray(this Transform from, Transform[] to , float speed = MoveTask.DefaultSpeed, Action endAction = null,float rotationSpeed = MoveTask.DefaultRotationSpeed,
                                        MoveType moveType = MoveType.towards , bool reverse = false , bool IsPlus = false , bool isloacal = false)
        {
            var transforms = to.ToList();
            if (reverse){
                transforms.Reverse();
            }

            var task = new MoveTask(from, transforms[0] , speed ,rotationSpeed, moveType ,IsPlus , isloacal , endAction);

            for (int i = 1; i < to.Length; i++)
            {
                task.Join(transforms[i]);
            }

            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask MoveLocal(this Transform from, Vector3 to, float speed = MoveTask.DefaultSpeed, Action endAction = null,
                                        bool IsPlus = false , MoveType moveType = MoveType.towards)
        {
            var task = new MoveTask(from, to, speed , moveType , IsPlus , true, endAction);

            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask Move(this Transform from, Transform to, float speed = MoveTask.DefaultSpeed, MoveType moveType = MoveType.towards,
                                    float rotationSpeed = MoveTask.DefaultRotationSpeed, bool IsPlus = false, Action endAction = null)
        {
            var task = new MoveTask(from, to, speed, rotationSpeed , moveType, IsPlus , false , endAction);
            AnilUpdate.Register(task);
            return task;
        }

        public static MoveTask Translate(this Transform from, Vector3 pos , Vector3 rot, float speed = MoveTask.DefaultSpeed, MoveType moveType = MoveType.towards,
                                    float rotationSpeed = MoveTask.DefaultRotationSpeed, bool IsPlus = false, Action endAction = null)
        {
            var task = new MoveTask(from, pos , rot , speed, rotationSpeed, moveType, IsPlus, false, endAction);
            AnilUpdate.Register(task);
            return task;
        }

    }
}