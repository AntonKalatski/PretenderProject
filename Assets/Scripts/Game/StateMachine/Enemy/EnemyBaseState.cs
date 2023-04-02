using Modules.StateMachine;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyBaseState : BaseState
    {
        protected readonly EnemyStateMachine EnemyStateMachine;
        
        protected EnemyBaseState(EnemyStateMachine enemyStateMachine) => EnemyStateMachine = enemyStateMachine;
        
        public override void Enter()
        {
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
        
        protected void Move()
        {
            Move(Vector3.zero);
        }

        protected void Move(Vector3 direction)
        {
            EnemyStateMachine.CharacterController.Move((direction + EnemyStateMachine.ForcesReceiver.Movement) * Time.deltaTime);
        }

        protected void FaceTarget()
        {
            if (EnemyStateMachine.Player is null) return;
            var transform = EnemyStateMachine.transform;
            var lookDirection = EnemyStateMachine.Player.transform.position - transform.position;
            if (lookDirection == Vector3.zero) return;
            lookDirection.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(lookDirection),
                EnemyStateMachine.MovementConfig.RotationSpeed * Time.deltaTime);
        }
        
        protected bool IsInAttackRange()
        {
            var playerPos = EnemyStateMachine.Player.transform.position;
            var transformPos = EnemyStateMachine.transform.position;
            var attackRange = EnemyStateMachine.AttackConfig.AttackRange;
            float sqrDistance = (playerPos - transformPos).sqrMagnitude;
            return sqrDistance <= attackRange * attackRange;
        }

        protected bool IsInChaseRange()
        {
            float sqrDistance = (EnemyStateMachine.transform.position - EnemyStateMachine.Player.transform.position).sqrMagnitude;
            return sqrDistance <= EnemyStateMachine.DetectionRange * EnemyStateMachine.DetectionRange;
        }
    }
}