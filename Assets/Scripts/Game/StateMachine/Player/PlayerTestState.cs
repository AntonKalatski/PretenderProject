using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private static readonly int _movementSpeed = Animator.StringToHash("MovementSpeed");

        public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            var transform = PlayerStateMachine.transform;
            var movementSpeed = PlayerStateMachine.MovementSpeed * Time.deltaTime;
            var rotationSpeed = PlayerStateMachine.RotationSpeed * Time.deltaTime;
            var movementValue = PlayerStateMachine.InputService.MovementValue;
            var direction = new Vector3(movementValue.x, 0f, movementValue.y);
            PlayerStateMachine.Animator.SetFloat(_movementSpeed, movementValue.magnitude, 0.1f, Time.deltaTime);
            PlayerStateMachine.CharacterController.Move(direction * movementSpeed);
            if (direction != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
        }

        public override void Exit()
        {
            Debug.Log(nameof(Exit));
            UnsubscribeInputEvents();
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

        private void OnJumpEventHandler() => PlayerStateMachine.SwitchState(new PlayerTestState(PlayerStateMachine));

        private void OnDodgeEventHandler()
        {
        }
    }
}