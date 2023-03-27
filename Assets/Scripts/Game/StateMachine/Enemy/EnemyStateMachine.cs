using Game.Configs.Enemy;
using Game.Forces;
using Modules.Services.PlayerService;
using Modules.StateMachine;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    public class EnemyStateMachine : BaseStateMachine
    {
        [field: SerializeField] public float DetectionRange { get; private set; }
        
        [field: SerializeField] public Animator Animator { get; private set; }
        
        [field: SerializeField] public EnemyMovementConfig MovementConfig { get; private set; }
        
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        
        [field: SerializeField] public ForcesReceiver ForcesReceiver { get; private set; }

        public GameObject Player { get; private set; }

        private void Start()
        {
            Player = PlayerService.Instance.Player;
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }
    }
}