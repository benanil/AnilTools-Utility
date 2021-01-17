using UnityEngine;

namespace AnilTools
{
    public static class RandomReal
    {
        private static float floatChace;
        private static int IntChace;
        private static short ShortChace;
        private static byte ByteChace;
        private static byte tryCount;

        public static Vector3 V3RandomizerMin0(this float value)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            return new Vector3(Rangef(0, value), Rangef(0, value), Rangef(0, value));
        }

        public static Vector3 V3RandomizerMin1(this float value)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            return new Vector3(Rangef(1, value), Rangef(1, value), Rangef(1, value));
        }

        // küp gibi random
        public static Vector3 V3Randomizer(Vector3 value)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            
            return new Vector3(Rangef(-value.x, value.x), Rangef(-value.y/2, value.y), Rangef(-value.z, value.z));
        }

        public static int Range(this Vector2 minMax)
        {
            Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
            return (int)Random.Range(minMax.x,minMax.y);
        }

        public static float Rangef(this Vector2 minMax)
        {
            Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
            return Random.Range(minMax.x, minMax.y);
        }

        public static int Range(int x, int y)
        {
            Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
            return Random.Range(x, y);
        }

        public static float Rangef(float x, float y)
        {
            Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
            return Random.Range(x, y);
        }

        public static int Range(this Vector2 minMax, int lastValue)
        {
            IntChace = lastValue;

            while (IntChace == lastValue)
            {
                Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
                IntChace = Mathf.RoundToInt(Random.Range(minMax.x, minMax.y));
            }

            return IntChace;
        }

        public static short Range(this Vector2 minMax, short lastValue)
        {
            ShortChace = lastValue;

            while (ShortChace == lastValue)
            {
                Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
                ShortChace = (short)Mathf.RoundToInt(Random.Range(minMax.x, minMax.y));
            }

            return ShortChace;
        }

        public static byte Range(this Vector2 minMax, byte lastValue)
        {
            ByteChace = lastValue;

            while (ByteChace == lastValue)
            {
                Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
                ByteChace = (byte)Mathf.RoundToInt(Random.Range(minMax.x, minMax.y));
            }

            return ByteChace;
        }

        public static int Range(int lastValue, Vector2 minMax)
        {
            IntChace = lastValue; 

            while (IntChace == lastValue)
            {
                Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
                IntChace = Mathf.RoundToInt(Random.Range(minMax.x, minMax.y));
            }

            return IntChace;
        }

        public static float Range(float lastValue, Vector2 minMax)
        {
            floatChace = lastValue;

            while (Mathf.Approximately(floatChace, lastValue))
            {
                Random.InitState((int)System.DateTime.Now.Ticks + tryCount++);
                floatChace = Random.Range(minMax.x, minMax.y);
            }

            return floatChace;
        }
    }
}