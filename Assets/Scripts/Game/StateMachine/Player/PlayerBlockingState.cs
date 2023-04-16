using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerBlockingState : PlayerBaseState
    {
        private static readonly int _block = Animator.StringToHash("Block");

        public PlayerBlockingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_block,
                PlayerStateMachine.MovementConfig.TransitionDuration);
            PlayerStateMachine.Health.SetIsBlocking(true);
            PlayerStateMachine.InputService.OnBlockEvent += OnBlockEventHandler;
        }

        public override void Tick()
        {
            Move();
        }

        public override void Exit()
        {
            PlayerStateMachine.Health.SetIsBlocking(false);
            PlayerStateMachine.InputService.OnBlockEvent -= OnBlockEventHandler;
        }

        private void OnBlockEventHandler(bool isBlocking)
        {
            if (isBlocking) return;

            if (PlayerStateMachine.Targeter.CurrentTarget == null)
            {
                PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
            }
            else
            {
                PlayerStateMachine.SwitchState(new PlayerTargetingState(PlayerStateMachine));
            }
        }
    }
}