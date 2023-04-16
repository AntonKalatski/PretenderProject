namespace Game.StateMachine.Player
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

        public override void Enter()
        {
            //toggle ragdoll
            PlayerStateMachine.CharacterController.enabled = false;
            PlayerStateMachine.Animator.enabled = false;
            PlayerStateMachine.Ragdoll.SetActiveRagdoll(true);
            PlayerStateMachine.WeaponHandler.DisableWeapon();
        }

        public override void Tick() { }

        public override void Exit() { }
    }
}