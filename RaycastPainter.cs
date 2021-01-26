
using UnityEngine;

namespace AnilTools.Paint
{
    public class RaycastPainter : MonoBehaviour
    {
        public Color color;
        public LayerMask layerMask;

        public int radius;
        public bool erase;

        private void Update()
        {
            if (InputPogo.MouseHold)
            {
                if (Mathmatic.RaycastFromCamera(out RaycastHit hit, Mathmatic.FloatMaxValue, layerMask))
                {
                    if (erase)
                        Painter.PaitCircale(hit, radius, color);
                    else
                        Painter.EraseCircale(hit, radius);
                }
            }
        }
    }
}