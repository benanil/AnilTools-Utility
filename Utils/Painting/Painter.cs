
using UnityEngine;

namespace AnilTools.Paint
{
    public static class Painter
    {
        static Texture2D texture2D;
        static Renderer renderer;
        static Vector2 pCoord;

        public static readonly int _MainTex = Shader.PropertyToID("_MainTex");
        
        public static void PaitCircale(RaycastHit hit, int radius , Color color)
        {
            var anilMaterial = hit.collider.GetComponent<AnilMaterial>();
            if (!anilMaterial)
                return;

            renderer = anilMaterial.renderer;

            texture2D = anilMaterial.texture2D;

            if (!texture2D.isReadable)
            {
                Debug2.Log("texture is not readable");
                return;
            }

            pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            // startPosX
            int x = Mathf.RoundToInt(pCoord.x * renderer.material.mainTextureScale.x);
            // startPosY
            int y = Mathf.RoundToInt(pCoord.y * renderer.material.mainTextureScale.y);

            float rSquared = radius * radius;

            for (int u = x - radius; u < x + radius + 1; u++)
                for (int v = y - radius; v < y + radius + 1; v++)
                    if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                        texture2D.SetPixel(u, v, color);

            texture2D.Apply();
            renderer.material.SetTexture(_MainTex, texture2D);
        }

        public static void EraseCircale(RaycastHit hit, int radius)
        {
            var anilMaterial = hit.collider.GetComponent<AnilMaterial>();
            if (!anilMaterial)
                return;

            renderer = anilMaterial.renderer;
            texture2D = anilMaterial.texture2D;

            if (!texture2D.isReadable)
            {
                Debug2.Log("texture is not readable");
                return;
            }

            pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;

            int x = Mathf.RoundToInt(pCoord.x * renderer.material.mainTextureScale.x);
            int y = Mathf.RoundToInt(pCoord.y * renderer.material.mainTextureScale.y);

            anilMaterial.Erase(x, y, radius);
        }

        public static void PaitCube(RaycastHit hit, int widthHeight, Color color)
        {
            var anilMaterial = hit.collider.GetComponent<AnilMaterial>();
            if (!anilMaterial)
                return;

            renderer = anilMaterial.renderer;
            texture2D = anilMaterial.texture2D;

            if (!texture2D.isReadable)
            {
                Debug2.Log("texture is not readable");
                return;
            }

            pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            // startPosX
            int xStart = Mathf.RoundToInt(pCoord.x * renderer.material.mainTextureScale.x);
            // startPosY
            int yStart = Mathf.RoundToInt(pCoord.y * renderer.material.mainTextureScale.y);

            for (int x = -widthHeight/2; x < widthHeight / 2; x++)
            {
                for (int y = -widthHeight / 2; y < widthHeight / 2; y++)
                {
                    texture2D.SetPixel(xStart + x, yStart + y, color);
                }
            }

            texture2D.Apply();
            renderer.material.SetTexture(_MainTex, texture2D);
        }

    }
}
