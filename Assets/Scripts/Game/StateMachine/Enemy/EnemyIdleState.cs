using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private static readonly int _movementBlendTree = Animator.StringToHash("Movement");
        private static readonly int _movementSpeed = Animator.StringToHash("MovementSpeed");


        public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_movementBlendTree,
                EnemyStateMachine.MovementConfig.TransitionDuration);
        }

        public override void Tick()
        {
            Move();
            
            if (IsInChaseRange())
            {
                EnemyStateMachine.SwitchState(new EnemyChasingState(EnemyStateMachine));
                return;
            }
            
            EnemyStateMachine.Animator.SetFloat(_movementSpeed, 0, 0.1f, Time.deltaTime);
        }

        public override void Exit()
        {
        }
    }
}