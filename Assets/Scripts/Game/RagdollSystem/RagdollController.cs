using Game.Constants;
using UnityEngine;

namespace Game.RagdollSystem
{
    public class RagdollController : MonoBehaviour
    {
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        private void Start()
        {
            _colliders = GetComponentsInChildren<Collider>(true);
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
            SetActiveRagdoll(false);
        }

        public void SetActiveRagdoll(bool isActive)
        {
            foreach (var collider in _colliders)
            {
                if (collider.gameObject.CompareTag(GameConstants.TagAndLayers.RAGDOLL))
                    collider.enabled = isActive;
            }
            foreach (var rigidBody in _rigidbodies)
            {
                if (rigidBody.gameObject.CompareTag(GameConstants.TagAndLayers.RAGDOLL))
                {
                    rigidBody.isKinematic = !isActive;
                    rigidBody.useGravity = isActive;
                }
            }
            
            //author disables character controller and animator from here
            // i don't think this is the responsibility of ragdoll control component
            //controller.enabled = !isActive;
            //animator.enabled = !isActive;
        }
    }
}