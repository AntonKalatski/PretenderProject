using UnityEngine;
using Cinemachine;
using System.Linq;
using System.Collections.Generic;

namespace Game.CombatSystem.Targeting
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup _targetGroup;

        private HashSet<Target> _targets = new();
        public Target CurrentTarget { get; private set; }

        public bool TrySelectTarget()
        {
            if (_targets.Count == 0) return false;
            CurrentTarget = _targets.First();
            _targetGroup.AddMember(CurrentTarget.transform, 1, 2); //weight and radius from config!
            return true;
        }

        public void ResetCurrentTarget()
        {
            if (ReferenceEquals(CurrentTarget, null)) return;
            _targetGroup.RemoveMember(CurrentTarget.transform); //weight and radius from config!
            CurrentTarget = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Target target)) return;
            if (!_targets.Add(target)) return;
            target.OnDestroyed += TargetDestroyHandler;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Target target)) return;
            TargetDestroyHandler(target);
        }

        private void TargetDestroyHandler(Target target)
        {
            if (CurrentTarget == target) ResetCurrentTarget();
            if (!_targets.Remove(target)) return;
            target.OnDestroyed -= TargetDestroyHandler;
        }
    }
}