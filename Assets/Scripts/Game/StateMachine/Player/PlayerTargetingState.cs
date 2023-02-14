namespace Game.StateMachine.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter() => SubscribeInputEvents();

        public override void Tick()
        {
        }

        public override void Exit() => UnsubscribeInputEvents();

        private void SubscribeInputEvents() => PlayerStateMachine.InputService.OnLockUnlockTargetEvent += OnLockUnlockTargetHandler;

        private void UnsubscribeInputEvents() => PlayerStateMachine.InputService.OnLockUnlockTargetEvent -= OnLockUnlockTargetHandler;

        private void OnLockUnlockTargetHandler()
        {
            PlayerStateMachine.SwitchState(new PlayerMovementState(PlayerStateMachine));
        }
    }
}