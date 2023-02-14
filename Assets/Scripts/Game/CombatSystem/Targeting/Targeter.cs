using UnityEngine;
using System.Collections.Generic;

namespace Game.CombatSystem.Targeting
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] private List<Target> _targets = new();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Target target)) return;
            _targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Target>(out var target)) return;
            if (_targets.Contains(target))
                _targets.Remove(target);
        }

        public void SelectTarget()
        {
            
        }
    }
}