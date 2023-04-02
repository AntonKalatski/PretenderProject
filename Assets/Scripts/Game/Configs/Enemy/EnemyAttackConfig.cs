using UnityEngine;

namespace Game.Configs.Enemy
{
    [CreateAssetMenu(menuName = "Game/Configs/Enemy/" + nameof(EnemyAttackConfig),
        fileName = nameof(EnemyAttackConfig), order = 1)]
    public class EnemyAttackConfig : ScriptableObject
    {
        [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
        [field: SerializeField] public int AttackDamage { get; private set; } = 10;
        
        [field: SerializeField] public int KnockBack { get; private set; } = 10;
    }
}