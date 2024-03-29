using System;
using UnityEngine;

namespace Game.HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;

        private int _currentHealth;
        private bool _isBlocking;
        public event Action OnDamageTaken;
        public event Action OnDie;

        public bool IsDead => _currentHealth == 0;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void DealDamage(int damage)
        {
            if (_currentHealth == 0) return;
            if (_isBlocking) return;
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnDamageTaken?.Invoke();
            if (_currentHealth == 0) OnDie?.Invoke();
            Debug.Log($"Name: {gameObject.name}/Health: {_currentHealth}");
        }

        public void SetIsBlocking(bool isBlocking) => _isBlocking = isBlocking;
    }
}