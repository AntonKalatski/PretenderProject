using Game.StateMachine.Player;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyImpactDamageState : EnemyBaseState
    {
        private readonly int _damageImpact = Animator.StringToHash("DamageImpact");
        private float _duration = 1f;

        public EnemyImpactDamageState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            //TODO this duration have to be in config
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_damageImpact, 0.1f);
        }

        public override void Tick()
        {
            Move();
            _duration -= Time.deltaTime;
            if (_duration <= 0) EnemyStateMachine.SwitchState(new EnemyIdleState(EnemyStateMachine));
        }

        public override void Exit()
        {
        }
    }
}