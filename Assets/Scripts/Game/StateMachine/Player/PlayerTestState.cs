using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _timer;

        public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log(nameof(Enter));
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            _timer += Time.deltaTime;
            Debug.Log(_timer);
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