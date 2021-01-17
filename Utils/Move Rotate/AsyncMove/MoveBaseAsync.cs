// https://github.com/Cysharp/UniTask
// bu özelliği aktif etmek için yukarıdaki asseti indiriniz
// aksi taktirde allttaki satırı commnt satırı yapınız
#define HasCysharpAsset
#if HasCysharpAsset
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using static Cysharp.Threading.Tasks.UniTask;

namespace AnilTools.MoveAsync
{
    using static AnilToolsBase;
    
    public static class MoveBaseAsync
    {
        /// <summary>
        /// performans için
        /// </summary>
        internal const float Tolerance = 0.05f;

#if HasCysharpAsset
        public static async UniTask TranslateLerp(this Transform From, TranslateData moveInfo)
        {
            await WhenAll(From.MoveLerp(moveInfo.To.position, moveInfo.MoveSpeed),
                          From.RotateLerp(new RotateData( moveInfo.To.rotation, moveInfo.RotateSpeed)));
        }

        public static async UniTask TranslateTowards(this Transform From, Transform to , float speed,float rotateSpeed = 10)
        {
            await WhenAll(From.MoveTowards(to.position, speed),
                          From.RotateLerp(new RotateData(to.rotation, rotateSpeed)));
        }

        public static async UniTask TranslateTowards(this Transform From,TranslateData moveInfo, Vector3 anchor)
        {
            await WhenAll(From.MoveTowards(moveInfo.To.position + anchor, moveInfo.MoveSpeed),
                          From.RotateLerp(new RotateData(moveInfo.To.rotation, moveInfo.RotateSpeed)));
        }

        public static async UniTask TranslateTowardsTime(this Transform From, TranslateData moveInfo,Vector3 anchor, byte time = 6)
        {
            await WhenAll(From.MoveTowards(moveInfo.To.position + anchor, moveInfo.MoveSpeed),
                          From.RotateLerp(new RotateData(moveInfo.To.rotation, moveInfo.RotateSpeed,time)));
        }

        public static async UniTask TranslateTowardsLocal(this Transform From, TranslateData moveInfo)
        {
            await WhenAll(From.MoveTowardsLocal(moveInfo.To.position, moveInfo.MoveSpeed),
                          From.RotateTowardsLocal(new RotateData(moveInfo.To.rotation, moveInfo.RotateSpeed)));
        }

        public static async UniTask SmoothLookat(Transform pos, Vector3 targetPos, float speed)
        {
            var targetDir = targetPos - pos.position;
            var targetRot = Quaternion.LookRotation(targetDir);

            while (Quaternion.Angle(pos.rotation, targetRot) > 1)
            {
                pos.rotation = Quaternion.RotateTowards(pos.rotation, targetRot, speed * Time.deltaTime);
                await Delay(UpdateSpeed);
            }
        }

#endif
        public readonly struct RotateData
        {
            public readonly Quaternion To;
            public readonly float Speed;
            public readonly float time;

            public RotateData(Quaternion to, float speed,float time = 6)
            {
                To = to;
                Speed = speed;
                this.time = time;
            }
        }

        public readonly struct TranslateData
        {
            public readonly Transform To;
            public readonly float MoveSpeed;
            public readonly float RotateSpeed;

            public TranslateData(Transform to, float moveSpeed, float rotateSpeed)
            {
                To = to;
                MoveSpeed = moveSpeed;
                RotateSpeed = rotateSpeed;
            }
        }


    }
}
