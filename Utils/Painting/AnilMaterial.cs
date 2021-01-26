using UnityEngine.Experimental.Rendering;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace AnilTools.Paint
{
    public class AnilMaterial : MonoBehaviour
    {
        const float ColorTolerance = 0.2f;
        [ReadOnly]
        public Texture2D texture2D;
        [ReadOnly]
        public Renderer renderer;

        private Texture2D oldTexture;

        [Header("Mehmet için")]
        public bool transparentToTexture;

        private void Start()
        {
            if (!renderer) renderer = GetComponent<Renderer>();
            
            oldTexture = renderer.material.mainTexture as Texture2D;

            texture2D = new Texture2D(oldTexture.height, oldTexture.width, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);

            if (transparentToTexture){
                var transparentColors = new Color32[oldTexture.height * oldTexture.width];
                transparentColors.Foreach(x => x = new Color32(0, 0, 0, 0));
                texture2D.SetPixels32(transparentColors);
            }
            else{
                texture2D.SetPixels32(oldTexture.GetPixels32());
            }

            texture2D.Apply();

            renderer.material.SetTexture(Painter._MainTex, texture2D);
        }

        public void Erase(int x , int y , int radius)
        {
            float rSquared = radius * radius;

            for (int u = x - radius; u < x + radius + 1; u++)
                for (int v = y - radius; v < y + radius + 1; v++)
                    if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                        texture2D.SetPixel(u, v, oldTexture.GetPixel(u,v));

            texture2D.Apply();
            renderer.material.SetTexture(Painter._MainTex, texture2D);
        }

        public int GetPercentage(Color color)
        {
            int trues = TruePixels(color);

            Debug.Log("percentage: " + (trues / texture2D.height * texture2D.width) * 100);

            return (trues / texture2D.height * texture2D.width) * 100;  
        }

        public bool CheckAllPainted(Color color, int PixelTolerance = 0)
        {
            int trues = TruePixels(color);
            Debug.Log("true pixels: " + trues);

            return trues >= texture2D.height * texture2D.width - PixelTolerance;
        }

        public int TruePixels(Color color)
        {
            int trues = 0;

            for (int x = 0; x < texture2D.height; x++)
                for (int y = 0; y < texture2D.width; y++)
                    if (texture2D.GetPixel(x, y).Difrance(color) < ColorTolerance)
                        trues++;
            return trues;
        }
    }

    [CustomEditor(typeof(AnilMaterial))]
    public class MaterialEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("save"))
            {
                var savedTex = ((AnilMaterial)target).texture2D.EncodeToPNG();
                // adminstator yerine kullanıcı adınız
                // istediğiniz yere kaydedin
                File.WriteAllBytes("C:/Users/Administrator/Desktop/save.png", savedTex);
            }
        }
    }

}