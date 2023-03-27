using Game.Configs.Base;
using UnityEngine;

namespace Game.Configs.Player
{
    [CreateAssetMenu(menuName = "Game/Configs/Player/" + nameof(PlayerMovementConfig),
        fileName = nameof(PlayerMovementConfig), order = 0)]
    public class PlayerMovementConfig : BaseMovementConfig
    {
    }
}