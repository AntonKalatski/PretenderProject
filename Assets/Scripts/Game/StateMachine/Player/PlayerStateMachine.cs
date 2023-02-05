using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}