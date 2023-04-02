using UnityEngine;
using UnityEngine.AI;

namespace Game.Forces
{
    public class ForcesReceiver : MonoBehaviour //TODO refactor this
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _drag = 0.3f;

        private float _verticalVelocity;
        private Vector3 _impactForce;
        private Vector3 _dampingVelocity;

        public Vector3 Movement => _impactForce + Vector3.up * _verticalVelocity;

        private void Update()
        {
            if (_verticalVelocity < 0f && _controller.isGrounded)
            {
                _verticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                _verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }

            _impactForce = Vector3.SmoothDamp(_impactForce, Vector3.zero, ref _dampingVelocity, _drag);

            if (_navMeshAgent != null)
                if (_impactForce == Vector3.zero)
                    _navMeshAgent.enabled = true;
        }

        public void AddForce(Vector3 force)
        {
            _impactForce += force;
            if (_navMeshAgent != null)
                _navMeshAgent.enabled = false;
        }
    }
}