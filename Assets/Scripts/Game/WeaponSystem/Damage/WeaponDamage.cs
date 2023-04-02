using System.Collections.Generic;
using Game.Forces;
using UnityEngine;
using Game.HealthSystem;

namespace Game.WeaponSystem.Damages
{
    public class WeaponDamage : MonoBehaviour//TODO refactor
    {
        [SerializeField] private Collider playerCollider;
        
        private readonly List<Collider> _colliders = new();
        private float _knockBack;
        private int _currentDamage = 10;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider == playerCollider) return;
            if(_colliders.Contains(collider)) return;
            _colliders.Add(collider);
            if (!collider.TryGetComponent(out Health health)) return;
            health.DealDamage(_currentDamage);
            if(!collider.TryGetComponent(out ForcesReceiver forcesReceiver)) return;
            var targetPosition = collider.transform.position;
            var myPosition = playerCollider.transform.position;
            var direction = (targetPosition - myPosition).normalized;
            forcesReceiver.AddForce(direction * _knockBack);
        }

        private void OnEnable()
        {
            _colliders?.Clear();
        }

        public void SetCurrentDamage(int damage, float knockBack)
        {
            _currentDamage = damage;
            _knockBack = knockBack;
        }
    }
}