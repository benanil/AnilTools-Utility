
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
        public static Ray RayMidleScreen => new Ray(camera.transform.position,camera.transform.forward * Hundered);
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

        public static float Clamp( float value, float min, float max)
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
            if (value > min) return min;
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

        static Texture2D texture2D;
        static Renderer renderer;
        static Vector2 pCoord;

        public static Color RaycastPointColor(RaycastHit hit)
        {
            renderer = hit.collider.GetComponent<MeshRenderer>();
            if (!renderer)
                return Color.white;

            texture2D = renderer.material.mainTexture as Texture2D;
            
            if (!texture2D.isReadable)
            {
                Debug2.Log("texture is not readable");
                return Color.white;
            }
            
            pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            return texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * renderer.material.mainTextureScale.x), Mathf.FloorToInt(pCoord.y * renderer.material.mainTextureScale.y));
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

        public static bool RaycastFromCamera(out RaycastHit hit)
        {
            return Physics.Raycast(Ray, out hit);
        }

        public static bool RaycastFromCamera(out RaycastHit hit, float MaxDistance, LayerMask layerMask)
        {
            return Physics.Raycast(Ray, out hit, MaxDistance, layerMask);
        }

        public static bool RaycastFromCameraMidle(out RaycastHit hit)
        {
            return Physics.Raycast(RayMidleScreen, out hit);
        }

        public static bool RaycastFromCameraMidle(out RaycastHit hit, float maxDistance, LayerMask layerMask)
        {
            return Physics.Raycast(RayMidleScreen, out hit, maxDistance , layerMask);
        }

    }

}
