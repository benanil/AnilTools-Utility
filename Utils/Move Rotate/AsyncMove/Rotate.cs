// https://github.com/Cysharp/UniTask
// bu özelliği aktif etmek için yukarıdaki asseti indiriniz
// aksi taktirde allttaki satırı commnt satırı yapınız
#define HasCysharpAsset
#if HasCysharpAsset
using Cysharp.Threading.Tasks;
using System;
#endif
using UnityEngine;

namespace AnilTools.MoveAsync
{
    using static MoveBaseAsync;
    using static AnilToolsBase;
    
    public static class Rotate
    {

#if HasCysharpAsset

        public static async UniTask RotateTowardsLocal(this Transform From, RotateData rotateData)
        {
            await DoTime(() => From.localRotation = Quaternion.Slerp(From.localRotation, rotateData.To, rotateData.Speed * Time.deltaTime),rotateData.time);
        }

        public static async UniTask RotateLerp(this Transform From, RotateData rotateData)
        {
            await DoTime(() => From.rotation = Quaternion.Slerp(From.rotation, rotateData.To, rotateData.Speed * Time.deltaTime), rotateData.time);
        }
        
        public static async UniTask RotateAlways(this Transform pos, Vector3 direction, float speed)
        {
            var rotation = Quaternion.Euler(direction * speed);

            while (true)
            {
                pos.rotation *= rotation;
                await UniTask.Delay(UpdateSpeed);
            }
        }
#endif
    }
}