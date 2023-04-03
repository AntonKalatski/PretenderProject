using Game.Configs.Enemy;
using Game.Forces;
using Game.HealthSystem;
using Game.WeaponSystem.Handlers;
using Modules.Services.PlayerService;
using Modules.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Game.StateMachine.Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        [field: SerializeField] public float DetectionRange { get; private set; }

        [field: SerializeField] public Animator Animator { get; private set; }

        [field: SerializeField] public EnemyMovementConfig MovementConfig { get; private set; }

        [field: SerializeField] public EnemyAttackConfig AttackConfig { get; private set; }

        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [field: SerializeField] public ForcesReceiver ForcesReceiver { get; private set; }

        [field: SerializeField] public NavMeshAgent Agent { get; private set; }

        [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }

        [field: SerializeField] public Health Health { get; private set; }

        public GameObject Player { get; private set; }

        private void Awake()
        {
            Player = PlayerService.Instance.Player;
            Agent.updatePosition = false;
            Agent.updateRotation = false;
            SwitchState(new EnemyIdleState(this));
            Health.OnDamageTaken += OnDamageTakenHandler;
        }

        private void OnDestroy() => Health.OnDamageTaken -= OnDamageTakenHandler;

        private void OnDamageTakenHandler() => SwitchState(new EnemyImpactDamageState(this));

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }
    }
}