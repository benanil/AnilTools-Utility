using System;
using UnityEngine;

namespace UrFairy
{
    public static class VectorExtensions
    {
        public static Vector3 X(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }

        public static Vector3 Y(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }
        
        public static Vector3 Z(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }
        
        public static Vector3 XPlus(this Vector3 v, float x)
        {
            v.x += x;
            return v;
        }

        public static Vector3 YPlus(this Vector3 v, float y)
        {
            v.y += y;
            return v;
        }

        public static Vector3 ZPlus(this Vector3 v, float z)
        {
            v.z += z;
            return v;
        }
        
        public static Vector3 XLerp(this Vector3 v, float x , float speed)
        {
            v.x = Mathf.Lerp(v.x, x, speed * Time.deltaTime);
            return v;
        }

        public static Vector3 YLerp(this Vector3 v, float y, float speed)
        {
            v.y = Mathf.Lerp(v.y, y, speed * Time.deltaTime);
            return v;
        }

        public static Vector3 ZLerp(this Vector3 v, float z, float speed)
        {
            v.z = Mathf.Lerp(v.z, z, speed * Time.deltaTime);
            return v;
        }

        public static Vector3 X(this Vector3 v, Func<float, float> f)
        {
            v.x = f(v.x);
            return v;
        }

        public static Vector3 Y(this Vector3 v, Func<float, float> f)
        {
            v.y = f(v.y);
            return v;
        }

        public static Vector3 Z(this Vector3 v, Func<float, float> f)
        {
            v.z = f(v.z);
            return v;
        }
    }
}