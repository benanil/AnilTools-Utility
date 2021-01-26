using UnityEngine;
using UnityEngine.Events;

namespace AnilTools.Events
{
    public class TrigerEnterEvent : CollisionEventBase
    {
        public LayerMask Layer;
        public UnityEvent Event;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer.Equals(Layer))
            {
                Event.Invoke();
            }
        }

    }
}