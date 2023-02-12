using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            var movementSpeed = PlayerStateMachine.MovementSpeed;
            var movementValue = PlayerStateMachine.InputService.MovementValue;
            Vector3 movement = new Vector3(movementValue.x, 0f, movementValue.y);
            PlayerStateMachine.CharacterController.Move(movement * (movementSpeed * Time.deltaTime));
            if (movement == Vector3.zero) return;
            PlayerStateMachine.CharacterController.transform.rotation = Quaternion.LookRotation(movement);
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