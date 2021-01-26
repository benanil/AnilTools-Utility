
using AnilTools.Update;
using UnityEngine;

namespace AnilTools.Move
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class CharacterAgent : MonoBehaviour
    {
        private CharacterController character;

        public float speed;
        public float RotateSpeed;

        private Vector3 target;

        public bool isStopped = true;

        private UpdateTask updateTask;

        private void Start()
        {
            character = GetComponent<CharacterController>();
            StartUpdate();
        }

        private void StartUpdate()
        { 
            updateTask = RegisterUpdate.UpdateWhile(
            () =>
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.position - target), RotateSpeed * Time.deltaTime);
                character.Move(transform.forward * speed * Time.deltaTime);
            },
            () => true);
        }

        public void Stop()
        {
            updateTask.Dispose();
        }

        public void SetDestination(Vector3 pos)
        {
            StartUpdate();
            target = pos;
        }

    }
}