using System;
using Game.CombatSystem.Data;
using Game.CombatSystem.Targeting;
using Game.Configs.Player;
using Game.Forces;
using Game.HealthSystem;
using Game.RagdollSystem;
using Game.WeaponSystem.Handlers;
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
        [field: SerializeField] public ForcesReceiver ForcesReceiver { get; private set; }

        [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }

        [field: SerializeField] public Health Health { get; private set; }
        
        [field: SerializeField] public RagdollController Ragdoll { get; private set; }
        
        [field: SerializeField] public AttackData[] AttackDatas { get; private set; }

        public Transform MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = Camera.main.transform;
            SwitchState(new PlayerMovementState(this));
            Health.OnDamageTaken += OnDamageTakenHandler;
            Health.OnDie += OnDieHandler;
        }

        private void OnDestroy()
        {
            Health.OnDamageTaken -= OnDamageTakenHandler;
            Health.OnDie -= OnDieHandler;
        }

        private void OnDamageTakenHandler() => SwitchState(new PlayerImpactDamageState(this));

        private void OnDieHandler() => SwitchState(new PlayerDeadState(this));
    }
}