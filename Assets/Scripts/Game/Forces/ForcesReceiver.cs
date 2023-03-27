using UnityEngine;

namespace Game.Forces
{
    public class ForcesReceiver : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
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
        }

        public void AddForce(Vector3 force)
        {
            _impactForce += force;
        }
    }
}