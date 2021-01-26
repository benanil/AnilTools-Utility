using UnityEngine;

namespace AnilTools.Events
{
    public class CollisionEventBase : MonoBehaviour
    {
        [SerializeField]
        protected Color Color;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}