using UnityEngine;

namespace AnilTools
{
    public class MagicMachine : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Color color;

        public float radius;
        public int iteration;

        [SerializeField] private float X;
        [SerializeField] private float Y;

        [SerializeField] private float atan;
        [SerializeField] private float value;

        [SerializeField]
        private bool update;

        public void OnDrawGizmos()
        {
            Gizmos.color = color;
            if (update)
                for (int i = 0; i < iteration; i++)
                {
                    offset = new Vector3(Mathf.Sin(X), Mathf.Atan2(X,Y) * Mathf.Rad2Deg, Mathf.Cos(X)) * radius;
                    Gizmos.DrawLine(transform.position, transform.position + offset);
                }
        }

        public void OnValidate()
        {
        }

    }
}