using System;
using Game.Configs.Enemy;
using Modules.StateMachine;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        
        [field: SerializeField] public EnemyMovementConfig MovementConfig { get; private set; }

        private void Start()
        {
            SwitchState(new EnemyIdleState(this));
        }
    }
}