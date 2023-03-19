using Modules.StateMachine;

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
    }
}