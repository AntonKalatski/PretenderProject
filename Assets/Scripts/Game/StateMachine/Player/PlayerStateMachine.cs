using System;
using Game.CombatSystem.Targeting;
using Game.Configs.Player;
using UnityEngine;
using Modules.StateMachine;

namespace Game.StateMachine.Player
{
    public class PlayerStateMachine : BaseStateMachine
    {
        [field: SerializeField] public InputService InputService { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public PlayerMovementConfig MovementConfig { get; private set; } // make through config provider etc
        [field: SerializeField] public Targeter Targeter { get; private set; } 

        public Transform MainCamera { get; private set; }

        private void Start()
        {
            MainCamera = Camera.main.transform;
            SwitchState(new PlayerMovementState(this));
        }
    }
}