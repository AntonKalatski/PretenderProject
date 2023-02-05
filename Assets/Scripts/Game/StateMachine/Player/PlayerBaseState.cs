using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public abstract class PlayerBaseState : BaseState
    {
        protected readonly PlayerStateMachine PlayerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
        }
    }
}