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
            Debug.Log(PlayerStateMachine.Targeter.CurrentTarget.name);
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