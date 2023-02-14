using UnityEngine;
using System.Collections.Generic;

namespace Game.CombatSystem.Targeting
{
    public class Targeter : MonoBehaviour
    {
        private List<Target> _targets = new();
        public Target CurrentTarget { get; private set; }

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

        public bool TrySelectTarget()
        {
            if(_targets.Count == 0) return false;
            CurrentTarget = _targets[0];
            return true;
        }

        public void ResetCurrentTarget()
        {
            if (!ReferenceEquals(CurrentTarget, null)) CurrentTarget = null;
        }
    }
}