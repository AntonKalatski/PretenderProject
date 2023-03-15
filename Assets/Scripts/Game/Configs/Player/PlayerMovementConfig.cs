using UnityEngine;

namespace Game.Configs.Player
{
    [CreateAssetMenu(menuName = "Game/Configs/Player/" + nameof(PlayerMovementConfig),
        fileName = nameof(PlayerMovementConfig), order = 0)]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 8f;
        [field: SerializeField, Range(0f, 1f)] public float TransitionDuration { get; private set; } = 0.25f;
    }
}