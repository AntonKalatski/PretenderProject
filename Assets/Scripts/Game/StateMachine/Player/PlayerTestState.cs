using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _movementSpeed = 5f;

        public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            var movementValue = PlayerStateMachine.InputService.MovementValue;
            Vector3 movement = new Vector3(movementValue.x, 0f, movementValue.y);
            PlayerStateMachine.transform.Translate(movement * _movementSpeed * Time.deltaTime);
            Debug.Log(PlayerStateMachine.InputService.MovementValue);
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