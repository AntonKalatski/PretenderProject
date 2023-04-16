using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
        
        public override void Enter()
        {
            //toggle ragdoll
            EnemyStateMachine.CharacterController.enabled = false;
            EnemyStateMachine.Animator.enabled = false;
            EnemyStateMachine.Ragdoll.SetActiveRagdoll(true);
            EnemyStateMachine.WeaponHandler.DisableWeapon();
            Object.Destroy(EnemyStateMachine.Target);
        }

        public override void Tick() { }

        public override void Exit() { }
    }
}