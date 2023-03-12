using System;
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
        private Camera _camera;
        public Target CurrentTarget { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
        }

        public bool TrySelectTarget()
        {
            if (_targets.Count == 0) return false;
            Target closestTarget = null;
            float closestTargetDistance = float.PositiveInfinity;

            foreach (var target in _targets)
            {
                Vector2 viewPos = _camera.WorldToViewportPoint(target.transform.position);
                if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) continue;

                Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
                float toCenterMagnitude = toCenter.sqrMagnitude;
                if (toCenterMagnitude < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = toCenterMagnitude;
                }
            }

            if (closestTarget is null) return false;

            CurrentTarget = closestTarget;
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