using System.Collections.Generic;
using UnityEngine;
using Game.HealthSystem;

namespace Game.WeaponSystem.Damages
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider playerCollider;
        [SerializeField] private int currentDamage;

        private List<Collider> _colliders = new();

        private void OnTriggerEnter(Collider collider)
        {
            if (collider == playerCollider) return;
            if(_colliders.Contains(collider)) return;
            _colliders.Add(collider);
            if (!collider.TryGetComponent(out Health health)) return;
            health.DealDamage(currentDamage);
        }

        private void OnEnable()
        {
            _colliders?.Clear();
        }

        public void SetCurrentDamage(int damage)
        {
            currentDamage = damage;
        }
    }
}