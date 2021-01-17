using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UrFairy;

namespace AnilTools
{
    public static class Extentions
    {
        // Navmesh Extentions
        public static void SetNav(this NavMeshAgent nav, bool value)
        {
            nav.isStopped = !value;
            nav.updateRotation = value;
        }

        // canvasGroup Extentions
        public static void SetVisible(this CanvasGroup canvasGroup , bool value)
        {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }

        // Transform Extentsions
        public static void ZeroTransform(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
        }

        // Vector Extentions

        /// <summary>
        /// <b><para>it will only change z axis</para> </b>
        /// it is better for Memory allocation
        /// </summary>
        public static Vector3 OnlyY(this Vector3 pos)
        {
            pos.z = 0;
            pos.x = 0;
            return pos;
        }

        /// <summary>
        /// <b><para>it will set x and z axis</para> </b>
        /// it is better for Memory allocation
        /// </summary>
        public static Vector3 WithoutY(this Vector3 pos,float x , float z)
        {
            pos.z = z;
            pos.x = x;
            return pos;
        }

        /// <summary>
        /// <b><para>it will only change z axis</para></b>
        /// it is better for Memory allocation
        /// </summary>
        public static Vector3 OnlyZ(this Vector3 pos, float value)
        {
            pos.x = 0;
            pos.y = 0;
            pos.z = value;
            return pos;
        }

        // Quaternion Extentions
        public static Quaternion OnlyY(this Quaternion quaternion)
        {
            quaternion.eulerAngles.X(0);
            quaternion.eulerAngles.Z(0);
            return quaternion;
        }

        // List Extentions
        public static void ForEachDoBreak<T>(this List<T> list, Action endAction, Func<bool> EndCondition)
        {
            for (byte i = 0; i < list.Count; i++){
                if (EndCondition.Invoke()){
                    endAction.Invoke();
                    break;
                }
            }
        }

        public static void ForEachBreak<T>(this List<T> list, Func<T,bool> endAction)
        {
            for (byte i = 0; i < list.Count; i++) {
                if (endAction.Invoke(list[i])){
                    break;
                }
            }
        }

        // String Extention
        public static bool Compare(this string word, string comparedWord , byte errorCount = 1)
        {
            byte equalCount = 0;
            for (byte i = 0; i < comparedWord.Length; i++){
                if (word[i].Equals(comparedWord[i])){
                    equalCount++;
                }
            }
            return equalCount >= word.Length -errorCount;
        }

        public static bool Equal(this string word, string comparedWord)
        {
            byte equalCount = 0;
            for (byte i = 0; i < comparedWord.Length; i++){
                if (word[i].Equals(comparedWord[i])){
                    equalCount++;
                }
            }
            return equalCount >= word.Length -1;
        }

        // Distance
        public static float DistanceSqr(this Transform transform, Vector3 pos)
        {
            return (pos - transform.position).sqrMagnitude;
        }

        public static float Distance(this Transform transform, Vector3 pos)
        {
            return Vector3.Distance(transform.position, pos);
        }

        public static float DistanceSqr(this Transform a, Transform b)
        {
            return (a.position - b.position).sqrMagnitude;
        }

        public static float Distance(this Transform a, Transform b)
        {
            return Vector3.Distance(a.position, b.position);
        }

        public static float DistanceSqr(this Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude;
        }
        
        public static float Distance(this Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a,b);
        }
    }
}
