using Game.Configs.Player;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerMovementState : PlayerBaseState
    {
        private static readonly int _movementSpeed = Animator.StringToHash("MovementSpeed");
        private readonly PlayerMovementConfig _movementConfig;
        private readonly Transform _transform;
        private float MovementSpeed => PlayerStateMachine.MovementConfig.MovementSpeed * Time.deltaTime;
        private float RotationSpeed =>  PlayerStateMachine.MovementConfig.RotationSpeed * Time.deltaTime;

        public PlayerMovementState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) => _transform = playerStateMachine.transform;

        public override void Enter() => SubscribeInputEvents();

        public override void Exit() => UnsubscribeInputEvents();

        public override void Tick()
        {
            var direction = CalculateMovement();
            PerformMovement(direction);
            PreformRotation(direction);
            PreformAnimation(direction);
        }

        private void SubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnJumpEvent += OnJumpEventHandler;
            PlayerStateMachine.InputService.OnDodgeEvent += OnDodgeEventHandler;
        }

        private void UnsubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnJumpEvent -= OnJumpEventHandler;
            PlayerStateMachine.InputService.OnDodgeEvent -= OnDodgeEventHandler;
        }

        private void OnJumpEventHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }

        private void OnDodgeEventHandler()
        {
        }

        private void PerformMovement(Vector3 direction)
        {
            PlayerStateMachine.CharacterController.Move(direction * MovementSpeed);
        }

        private void PreformAnimation(Vector3 direction)
        {
            PlayerStateMachine.Animator.SetFloat(_movementSpeed, direction.magnitude, 0.1f, Time.deltaTime);
        }

        private void PreformRotation(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(direction), RotationSpeed);
        }

        private Vector3 CalculateMovement()
        {
            var movement = PlayerStateMachine.InputService.MovementValue;
            var forward = PlayerStateMachine.MainCamera.forward;
            var right = PlayerStateMachine.MainCamera.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();
            return forward * movement.y + right * movement.x;
        }
    }
}