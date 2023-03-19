using System.Collections.Generic;
using UnityEngine;
using Game.HealthSystem;

namespace Game.WeaponSystem.Damages
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider playerCollider;
        
        private List<Collider> _colliders = new();

        private void OnTriggerEnter(Collider collider)
        {
            if (collider == playerCollider) return;
            if(_colliders.Contains(collider)) return;
            _colliders.Add(collider);
            if (!collider.TryGetComponent<Health>(out var health)) return;
            health.DealDamage(10);
        }

        private void OnEnable()
        {
            _colliders?.Clear();
        }
    }
}