using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        [field: SerializeField] public InputService InputService { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;

        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}