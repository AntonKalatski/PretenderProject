using UnityEngine;

namespace Game.HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void DealDamage(int damage)
        {
            if (_currentHealth == 0) return;
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            Debug.Log($"Name: {gameObject.name}");
            Debug.Log($"Health: {_currentHealth}");
        }
    }
}