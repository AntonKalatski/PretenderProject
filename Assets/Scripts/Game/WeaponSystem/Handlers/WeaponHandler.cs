using Game.WeaponSystem.Damages;
using UnityEngine;

namespace Game.WeaponSystem.Handlers
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private WeaponDamage _weaponDamage;
        [SerializeField] private GameObject _weapon;

        public void SetWeaponDamage(int damage)
        {
            _weaponDamage.SetCurrentDamage(damage);
        }
        
        public void EnableWeapon()
        {
            _weapon.SetActive(true);
        }
        
        public void DisableWeapon()
        {
            _weapon.SetActive(false);
        }
    }
}