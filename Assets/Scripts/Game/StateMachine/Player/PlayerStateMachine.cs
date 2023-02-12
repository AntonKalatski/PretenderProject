using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        [field: SerializeField] public InputService InputService { get; private set; }

        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}