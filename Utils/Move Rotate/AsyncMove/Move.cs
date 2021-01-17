// https://github.com/Cysharp/UniTask
// bu özelliği aktif etmek için yukarıdaki asseti indiriniz
// aksi taktirde allttaki satırı commnt satırı yapınız
#define HasCysharpAsset

// bu objeyi sürekli verilen rotada gezdirmek içindir
//#define UseLoop

#if HasCysharpAsset
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using UnityEngine;
using IEnumerator =  System.Collections.IEnumerator;

namespace AnilTools.MoveAsync
{
    using static AnilToolsBase;
    using static MoveBaseAsync;
    
    public static class Move
    {
#if HasCysharpAsset
        /// <summary>
        /// this will move object a to b with 1 line of code
        /// </summary>
        public static async UniTask MoveLerp(this Transform From, Vector3 movePos, float speed)
        {
            do
            {
                From.position = QuickLerp3(From.position, movePos, speed * Time.deltaTime);
                await UniTask.Delay(UpdateSpeed);
            } while (From.DistanceSqr(movePos) >= Tolerance);
        }

        /// <summary>
        /// this will move object a to b with 1 line of code
        /// </summary>
        public static async UniTask MoveTowards(this Transform From, Vector3 movePos, float speed)
        {
            do
            {
                From.position = Vector3.MoveTowards(From.position, movePos, speed * Time.deltaTime);
                await UniTask.Delay(UpdateSpeed);
            } while (From.DistanceSqr(movePos) >= Tolerance);
        }

        /// <summary>
        /// this will move object a to b with 1 line of code
        /// </summary>
        public static async UniTask MoveTowardsLocal(this Transform From, Vector3 movePos , float speed)
        {
            do
            {
                From.localPosition = Vector3.MoveTowards(From.localPosition, movePos, speed * Time.deltaTime);
                await UniTask.Delay(UpdateSpeed);
            } while (From.DistanceSqr(movePos) >= Tolerance);
            
        }

        /// <summary>
        /// this will move object a to b with 1 line of code
        /// </summary>
        public static async UniTask MoveAlways(this Transform pos, Vector3 direction, float speed)
        {
            while (true)
            {
                pos.position += direction * speed;
                await UniTask.Delay(UpdateSpeed);
            }
        }

        /// <summary>
        /// this will move transform all of the given points 
        /// </summary>
        public static IEnumerator MoveArray(this Transform from, Vector3[] positions,float speed, bool reverse = false, Action afterEvent = null)
        {
            var queue = new Queue<Vector3>(positions);

            if (reverse)
                queue.Reverse();

            while (queue.Count > 0)
            {
                yield return from.MoveTowards(queue.Dequeue(), speed);
            }
            
            if (afterEvent != null)
                afterEvent.Invoke();
        }
        /// <summary>
        /// this will move transform all of the given points 
        /// </summary>
        public static IEnumerator MoveArray(this Transform from, Transform[] transforms, float speed , bool reverse = false , Action afterEvent = null)
        {
            var queue = new Queue<Transform>(transforms);
            if (reverse)
                queue.Reverse();

            while (queue.Count > 0)
            {
                yield return from.MoveTowards(queue.Dequeue().position, speed);
            }

            if (afterEvent != null)
                afterEvent.Invoke();
        }

#if UseLoops
        /// <summary>
        /// this will trugh points
        /// </summary>
        public static IEnumerator MoveLoop(this Transform from, Vector3[] positions, float speed, bool reverse = false)
        {
            var queue = new Queue<Vector3>(positions);

            if (reverse)
                queue.Reverse();

            Vector3 position;

            while (queue.Count > 0)
            {
                position = queue.Dequeue();
                yield return from.MoveTowards(position, speed);
                queue.Enqueue(position);
            }
        }
        */
        /// <summary>
        /// this will trugh points
        /// </summary>
        public static IEnumerator MoveLoop(this Transform from, Transform[] positions, float speed, bool reverse = false)
        {
            var queue = new Queue<Transform>(positions);

            if (reverse)
                queue.Reverse();

            Transform transform;

            while (queue.Count > 0)
            {
                transform = queue.Dequeue();
                yield return from.MoveTowards(transform.position, speed);
                queue.Enqueue(transform);
            }
        }
#endif
        /// <summary>
        /// this is alternative way of vector3.lerp
        /// </summary>
        public static Vector3 QuickLerp3(Vector3 a, Vector3 b, float t)
        {
            return a * (1 - t) + b * t;
        }

#endif
    }
}