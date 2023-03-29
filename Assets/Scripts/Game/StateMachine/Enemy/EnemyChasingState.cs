using Modules.Services.PlayerService;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private static readonly int _movementBlendTree = Animator.StringToHash("Movement");
        private static readonly int _movementSpeed = Animator.StringToHash("MovementSpeed");


        public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_movementBlendTree,
                EnemyStateMachine.MovementConfig.TransitionDuration);
        }

        public override void Tick()
        {
            if (!IsInChaseRange())
            {
                EnemyStateMachine.SwitchState(new EnemyIdleState(EnemyStateMachine));
                return;
            }

            MoveToPlayer();
            
            EnemyStateMachine.Animator.SetFloat(_movementSpeed, 1, 0.1f, Time.deltaTime);
        }

        private void MoveToPlayer()
        {
            EnemyStateMachine.Agent.destination = EnemyStateMachine.Player.transform.position;
            var desiredVelocity = EnemyStateMachine.Agent.desiredVelocity.normalized;
            var direction = desiredVelocity * EnemyStateMachine.MovementConfig.MovementSpeed;
            Move(direction);
            EnemyStateMachine.Agent.velocity = EnemyStateMachine.CharacterController.velocity;
        }

        public override void Exit()
        {
            EnemyStateMachine.Agent.ResetPath();
            EnemyStateMachine.Agent.velocity = Vector3.zero;
        }
    }
}