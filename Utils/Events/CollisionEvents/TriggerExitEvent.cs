
using UnityEngine;
using UnityEngine.Events;

namespace AnilTools.Events
{
    public class TriggerExitEvent : CollisionEventBase
    {
        public LayerMask layer;
        public UnityEvent unityEvent;

        private void OnTriggerExit(Collider other)
        {
            if (layer.Contains(other.gameObject.layer))
            {
                unityEvent.Invoke();
            }
        }
    }
}