using Game.Configs.Base;
using UnityEngine;

namespace Game.Configs.Enemy
{
    [CreateAssetMenu(menuName = "Game/Configs/Enemy/" + nameof(EnemyMovementConfig),
        fileName = nameof(EnemyMovementConfig), order = 0)]
    public class EnemyMovementConfig : BaseMovementConfig
    {
    }
}