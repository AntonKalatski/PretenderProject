using System;
using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        [field: SerializeField] public InputService InputService { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 1f;

        private void Start()
        {
            SwitchState(new PlayerTestState(this));
        }
    }
}