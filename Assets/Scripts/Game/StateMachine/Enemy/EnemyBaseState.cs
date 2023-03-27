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

        protected bool IsInChaseRange()
        {
            float sqrDistance = (EnemyStateMachine.transform.position - EnemyStateMachine.Player.transform.position).sqrMagnitude;
            return sqrDistance <= EnemyStateMachine.DetectionRange * EnemyStateMachine.DetectionRange;
        }
    }
}