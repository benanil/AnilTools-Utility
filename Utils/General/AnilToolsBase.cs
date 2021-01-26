
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

namespace AnilTools
{
    public static class AnilToolsBase
    {
        public const int UpdateSpeed = 20;
        public const int UpdateSlow = 35;

        public async static UniTask DoTime(Action action , float time , UpdateType updateType = UpdateType.normal)
        {
            switch (updateType)
            {
                case UpdateType.fixedTime:
                    while (time > 0){
                        time -= Time.fixedDeltaTime;
                        action.Invoke();
                        await UniTask.WaitForFixedUpdate();
                    }
                    break;
                case UpdateType.normal:
                    while (time > 0) {
                        time -= Time.deltaTime;
                        action.Invoke();
                        await UniTask.Delay(UpdateSpeed);
                    }
                    break;
            }
        }

        public static IEnumerator DoFor(Action action, short Count, float waitTime = .5f)
        {
            var time = new WaitForSecondsRealtime(waitTime);

            for (short i = 0; i < Count; i++)
            {
                action.Invoke();
                yield return time;
            }
        }
    }
}
