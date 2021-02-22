using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnilTools
{
    public static class AsyncUpdate
    {
        public static void Update(this MonoBehaviour monobehaviour, float UpdateTime, Action action, Func<bool> EndCondition, Action endAction, UpdateType updateType)
        {
            monobehaviour.StartCoroutine(UpdateCoroutne(UpdateTime, action, EndCondition, endAction, updateType));
        }
        private static IEnumerator UpdateCoroutne(float UpdateTime, Action action, Func<bool> EndCondition, Action endAction, UpdateType updateType)
        {

            if (updateType == UpdateType.SlowUpdate)
            {
                WaitForSecondsRealtime time = new WaitForSecondsRealtime(UpdateTime);
                while (true)
                {
                    action.Invoke();
                    if (EndCondition != null)
                        if (EndCondition.Invoke())
                        {
                            endAction?.Invoke();
                            break;
                        }

                    yield return time;
                }
            }
            else //normal
            {
                while (true)
                {
                    action.Invoke();
                    if (EndCondition != null)
                        if (EndCondition.Invoke())
                        {
                            endAction?.Invoke();
                            break;
                        }

                    yield return null;
                }
            }
        }

        public static CoroutineData<int> Delay(this MonoBehaviour g, int frames, Action f)
        {
            var coroutineData = new CoroutineData<int>(f, frames);
            g.StartCoroutine(DelayCoroutine(frames, coroutineData));
            return coroutineData;
        }

        public static CoroutineData<float> Delay(this MonoBehaviour g, float seconds, Action f)
        {
            var coroutineData = new CoroutineData<float>(f, seconds);
            g.StartCoroutine(DelayCoroutine(seconds, coroutineData));
            return coroutineData;
        }

        private static IEnumerator DelayCoroutine(int frames, CoroutineData<int> f)
        {
            for (var n = 0; n < frames; ++n)
            {
                yield return null;
            }

            f.Invoke();

            while (true)
            {
                if (f.queue.Count != 0)
                {
                    for (int i = 0; i < f.currentAction.value1; i++)
                    {
                        yield return null;
                    }
                    f.Dequeue();
                    f.Invoke();
                }
                else
                {
                    f.Dispose();
                    break;
                }
            }

        }

        private static IEnumerator DelayCoroutine(float seconds, CoroutineData<float> f)
        {
            yield return new WaitForSeconds(seconds);

            f.Invoke();

            while (true)
            {
                if (f.queue.Count != 0)
                {
                    yield return new WaitForSecondsRealtime(f.currentAction.value1);
                    f.Dequeue();
                    f.Invoke();
                }
                else
                {
                    f.Dispose();
                    break;
                }
            }
        }

        public struct CoroutineData<T> : IDisposable // int or float
        {
            internal Queue<Tuple2<Action, T>> queue;
            internal Tuple2<Action, T> currentAction;

            public CoroutineData(Action action, T delay)
            {
                queue = new Queue<Tuple2<Action, T>>();
                currentAction = new Tuple2<Action, T>(action, delay);
            }

            public CoroutineData<T> Add(Action action, T delay)
            {
                queue.Enqueue(new Tuple2<Action, T>(action, delay));
                return this;
            }

            public void Invoke()
            {
                currentAction.value.Invoke();
            }

            internal void Dequeue()
            {
                currentAction = queue.Dequeue();
            }

            public void Dispose()
            {
                queue = null;
            }
        }
    }

}