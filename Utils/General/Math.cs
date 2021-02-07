
using UnityEngine;
using UnityEngine.AI;
using AnilTools.AnilEditor;
using static AnilTools.RandomReal;

namespace AnilTools
{
    public static class Mathmatic
    {
        #region prop & fields
        internal const byte Hundered = 100;
        private const float Half = .5f;
        public static Ray Ray => camera.ScreenPointToRay(Input.mousePosition);
        public static Ray RayMidleScreen => new Ray(camera.transform.position, camera.transform.forward * Hundered);
        public static Camera camera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = Camera.main;
                }
                return _camera;
            }
        }
        private static Camera _camera;
        private const int RandomDistance = 10;
        private static NavMeshHit meshHit;
        #endregion

        public const float FloatMaxValue = 2048;
        public const short IntMaxValue = 4056;

        public const float PIto90Degree = 90 / Mathf.PI;
        public const float PIto180Degree = 180 / Mathf.PI;

        public const float PISqr = Mathf.PI * 2;

        /// <summary> if value is plus value returns minus othervise return plus </summary>
        public static float Inverse(this float value)
        {
            return value < 0 ? Mathf.Abs(value) : -value;
        }

        /// <summary> if value is plus value returns minus othervise return plus </summary>
        public static float Inverse(this int value)
        {
            return value < 0 ? Mathf.Abs(value) : -value;
        }

        public static float Remap(this float value, float FirstMin, float FirstMax, float SecondMin, float SecondMax)
        {
            return (value - FirstMin) / (FirstMax - FirstMin) * (SecondMax - SecondMin) + SecondMin;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        public static float Clamp01(float value)
        {
            if (value < 0)
                return 0;
            if (value > 1)
                return 1;

            return value;
        }

        public static int Clamp(ref int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        #region Min Max

        // instagramda 2 tarafa girilen oyun yüzdesini hesaplanak için kullanılan sistem
        public static Tuple2<float, float>Calculate2Percent(float firstCount, float secondCount)
        {
            var OnePice = (firstCount + secondCount) / 100;
            return new Tuple2<float, float>(firstCount / OnePice, secondCount / OnePice);
        }

        // 3 partiye velilen oy listesi
        public static Tuple3<float, float , float> Calculate3Percent(float firstCount, float secondCount,float thirdCount)
        {
            var OnePice = (firstCount + secondCount + thirdCount) / 100;
            
            return new Tuple3<float, float , float>(firstCount/OnePice, secondCount/OnePice, thirdCount/OnePice);
        }

        /// <summary>
        /// bu değerden yukarı şıkmasını engeller </summary>
        public static int Max(this int value,int max = 1)
        {
            if (value > max) return max;
            else return value;
        }

        public static float Max(ref float value, float max = 1)
        {
            if (value > max)
            {
                value = max;
                return max;
            }
            else
            {
                return value;
            }
        }

        public static float Max(this float value, float max)
        {
            if (value > max) return max;
            else return value;
        }

        /// <summary>
        /// bu değerden aşşağı inmesini engeller
        /// </summary>
        public static int Min(this int value, int min)
        {
            if (value > min) return min;
            else return value;
        }

        public static float Min(this float value, float min)
        {
            if (value < min) return min;
            else return value;
        }

        #endregion

        /// <summary>
        ///  2 sayı arasındaki farkı girer
        /// </summary>
        public static float Difrance(this float a, float b)
        {

            return Mathf.Abs(a - b);
        }

        public static int Difrance(this int a,int b)
        {
            return Mathf.Abs(a - b);
        }

        public static float HalfPower(this float value)
        {
            return Mathf.Pow(value, Half);
        }

        // --- Vectors ---
        public static Vector3 FindSamplePos(Vector3 pos, Vector3 randomPos)
        {
            var SamplePos = Vector3.zero;
            // sonra silen
            short TryCount = 0; 

            while (SamplePos == Vector3.zero)
            {
                TryCount++;
                if (TryCount > 5)
                {
#if UNITY_EDITOR
                    Analysis.AddProblem("Navmesh sample bulunurken sorun yaşandı");
#endif
                    break;
                }
                if (NavMesh.SamplePosition(pos + V3Randomizer(randomPos) , out meshHit , RandomDistance , 1))
                {
                    return meshHit.position;
                }
            }

            return pos;
        }


        public static Vector3 ClosestPointToCircale(Vector3 origin, Vector3 target, float Radius)
        {
            var ray = new Ray(origin, (origin - target).normalized);
            return ray.GetPoint(-Radius);
        }

        public static Vector3 FindSamplePos(Vector3 pos, Vector3 randomPos, float maxy)
        {
            var SamplePos = Vector3.zero;
            // sonra silen
            short TryCount = 0;

            while (SamplePos == Vector3.zero)
            {
                TryCount++;
                if (TryCount > 10)
                    break;

                if (NavMesh.SamplePosition(pos + V3Randomizer(randomPos), out meshHit, RandomDistance, 1))
                {
                    if (meshHit.position.y < maxy)
                    return meshHit.position;
                }
            }

            return pos;
        }

        public static Vector3 Direction(Vector3 origin, Vector3 Point)
        {
            return (origin - Point).normalized;
        }

        private static class RointInfo
        { 
            public static Texture2D texture2D;
            public static Renderer renderer;
            public static Vector2 pCoord;
        }

        public static Color RaycastPointColor(RaycastHit hit)
        {
            RointInfo.renderer = hit.collider.GetComponent<MeshRenderer>();
            if (!RointInfo.renderer)
                return Color.white;

            RointInfo.texture2D = RointInfo.renderer.material.mainTexture as Texture2D;
            
            if (!RointInfo.texture2D.isReadable)
            {
                Debug2.Log("texture is not readable");
                return Color.white;
            }

            RointInfo.pCoord = hit.textureCoord;
            RointInfo.pCoord.x *= RointInfo.texture2D.width;
            RointInfo.pCoord.y *= RointInfo.texture2D.height;
            return RointInfo.texture2D.GetPixel(Mathf.FloorToInt(RointInfo.pCoord.x * RointInfo.renderer.material.mainTextureScale.x), Mathf.FloorToInt(RointInfo.pCoord.y * RointInfo.renderer.material.mainTextureScale.y));
        }

        /// <summary>
        /// girilen 2 değerin range içinde olup olmadığını döner
        /// </summary>
        public static bool InRange(Vector3 pos, Vector3 pos1,float range)
        {
            return (pos - pos1).sqrMagnitude.HalfPower() <= range;
        }

        static float dist;

        /// <summary>
        /// girilen 2 değerin range içinde olup olmadığını döner
        /// </summary>
        public static bool InRange(Vector3 pos, Vector3 pos1, ref float range)
        {
            dist = (pos - pos1).sqrMagnitude.HalfPower();

            return dist < range;
        }

        public static bool RaycastFromCamera(out RaycastHit hit, float MaxDistance = 2048, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore)
        {
            return Physics.Raycast(Ray, out hit, MaxDistance, layerMask, queryTriggerInteraction);
        }

        public static bool RaycastFromCameraMidle(out RaycastHit hit, float maxDistance, LayerMask layerMask)
        {
            return Physics.Raycast(RayMidleScreen, out hit, maxDistance , layerMask);
        }
    }

}

