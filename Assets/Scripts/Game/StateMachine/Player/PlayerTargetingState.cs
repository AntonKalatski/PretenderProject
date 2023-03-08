using UnityEngine;

namespace Game.StateMachine.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private static readonly int _targetingBlendTree = Animator.StringToHash("Targeting");

        public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            PlayerStateMachine.Animator.Play(_targetingBlendTree);
            SubscribeInputEvents();
        }

        public override void Tick()
        {
            if (PlayerStateMachine.Targeter.CurrentTarget is not null) return;
            PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }

        public override void Exit()
        {
            UnsubscribeInputEvents();
            PlayerStateMachine.Targeter.ResetCurrentTarget();
        }

        private void SubscribeInputEvents() =>
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent += OnLockUnlockTargetHandler;

        private void UnsubscribeInputEvents() =>
            PlayerStateMachine.InputService.OnLockUnlockTargetEvent -= OnLockUnlockTargetHandler;

        private void OnLockUnlockTargetHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }
    }
}