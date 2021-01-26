
using UnityEngine;
using UnityEngine.Events;

namespace AnilTools.Events
{
    public class CollisionExitEvent : CollisionEventBase
    {
        public LayerMask layer;
        public UnityEvent unityEvent;

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer.Equals(layer))
            {
                unityEvent.Invoke();
            }
        }

    }
}