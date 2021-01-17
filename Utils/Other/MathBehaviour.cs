using Cysharp.Threading.Tasks;
using UnityEngine;
using AnilTools.AnilEditor;
using static AnilTools.AnilToolsBase;

namespace AnilTools
{
    public abstract class MathBehaviour <T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
        
        [Header("Math")]
        [BackgroundColor(BGColors.red)]
        public float LerpValue;
        public float LerpValue1;
        public float LerpSpeed;

        protected virtual void Lerp(float target)
        {
            LerpValue = Mathf.Lerp(LerpValue, target, LerpSpeed);
        }

        protected virtual void Lerp1(float target)
        {
            LerpValue1 = Mathf.Lerp(LerpValue1, target, LerpSpeed);
        }

        public virtual async UniTask LerpTowards(float target, float speed = 0.08f)
        {
            if (target > LerpValue)
            {
                do
                {
                    LerpValue += speed;
                    await UniTask.Delay(UpdateSpeed);
                } while (true);
            }
            else
            {
                do
                {
                    LerpValue -= speed;
                    await UniTask.Delay(UpdateSpeed);
                } while (target < LerpValue);
            }
        }

    }
}