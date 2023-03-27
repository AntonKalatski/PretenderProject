using UnityEngine;

namespace Game.Configs.Base
{
    public abstract class BaseMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 8f;
        [field: SerializeField, Range(0f, 1f)] public float TransitionDuration { get; private set; } = 0.25f;
    }
}