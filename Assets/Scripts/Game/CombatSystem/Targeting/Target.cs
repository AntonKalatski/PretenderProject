using System;
using UnityEngine;

namespace Game.CombatSystem.Targeting
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyed;

        private void OnDestroy() => OnDestroyed?.Invoke(this);
    }
}