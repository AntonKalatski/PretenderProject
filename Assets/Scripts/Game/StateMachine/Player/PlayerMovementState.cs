using Game.Configs.Player;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerMovementState : PlayerBaseState
    {
        private static readonly int _movementSpeed = Animator.StringToHash("MovementSpeed");
        private static readonly int _movementBlendTree = Animator.StringToHash("Movement");

        private readonly PlayerMovementConfig _movementConfig;
        private readonly Transform _transform;

        public PlayerMovementState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) =>
            _transform = playerStateMachine.transform;

        public override void Enter()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_movementBlendTree,
                PlayerStateMachine.MovementConfig.TransitionDuration);
            SubscribeInputEvents();
        }

        public override void Exit()
        {
            UnsubscribeInputEvents();
        }

        public override void Tick()
        {
            var direction = CalculateMovement();
            Move(direction * PlayerStateMachine.MovementConfig.MovementSpeed);
            PreformRotation(direction);
            PreformAnimation(direction);
        }

        private void SubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnJumpEvent += OnJumpEventHandler;
            PlayerStateMachine.InputService.OnDodgeEvent += OnDodgeEventHandler;
            PlayerStateMachine.InputService.OnAttackEvent += OnAttackEventHandler;
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent += OnLockUnlockTargetHandler;
        }

        private void UnsubscribeInputEvents()
        {
            PlayerStateMachine.InputService.OnJumpEvent -= OnJumpEventHandler;
            PlayerStateMachine.InputService.OnDodgeEvent -= OnDodgeEventHandler;
            PlayerStateMachine.InputService.OnAttackEvent -= OnAttackEventHandler;
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent -= OnLockUnlockTargetHandler;
        }

        private void OnJumpEventHandler()
        {
            // PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }

        private void OnDodgeEventHandler()
        {
        }

        private void OnAttackEventHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerAttackingState(PlayerStateMachine, 0));
        }

        private void OnLockUnlockTargetHandler()
        {
            if (!PlayerStateMachine.Targeter.TrySelectTarget()) return;
            PlayerStateMachine.SwitchState(new PlayerTargetingState(PlayerStateMachine));
        }

        private void PreformAnimation(Vector3 direction)
        {
            PlayerStateMachine.Animator.SetFloat(_movementSpeed, direction.magnitude, 0.1f, Time.deltaTime);
        }

        private void PreformRotation(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(direction),
                PlayerStateMachine.MovementConfig.RotationSpeed * Time.deltaTime);
        }

        private Vector3 CalculateMovement()
        {
            var movement = PlayerStateMachine.InputService.MovementValue;
            var cameraForward = PlayerStateMachine.MainCamera.forward;
            var cameraRight = PlayerStateMachine.MainCamera.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            return cameraForward * movement.y + cameraRight * movement.x;
        }
    }
}