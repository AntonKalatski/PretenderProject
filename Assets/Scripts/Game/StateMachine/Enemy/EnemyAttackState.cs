using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private static readonly int _attackAnimation = Animator.StringToHash("Attack");
        private const string ATTACK_ANIMATION_TAG = "Attack";

        public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_attackAnimation,
                EnemyStateMachine.MovementConfig.TransitionDuration);
            EnemyStateMachine.WeaponHandler.SetWeaponDamage(EnemyStateMachine.AttackConfig.AttackDamage,
                EnemyStateMachine.AttackConfig.KnockBack);
        }

        public override void Tick()
        {
            var animator = EnemyStateMachine.Animator;
            if (GetNormalizedTime(animator, ATTACK_ANIMATION_TAG, ATTACK_ANIMATION_TAG) >= 1)
                EnemyStateMachine.SwitchState(new EnemyChasingState(EnemyStateMachine));
            FaceTarget();
        }

        public override void Exit()
        {
        }
    }
}