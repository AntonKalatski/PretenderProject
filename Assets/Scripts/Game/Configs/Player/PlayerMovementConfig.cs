using UnityEngine;

namespace Game.Configs.Player
{
    [CreateAssetMenu(menuName = "Game/Configs/Player/" + nameof(PlayerMovementConfig), fileName = nameof(PlayerMovementConfig), order = 0)]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 8f;
    }
}