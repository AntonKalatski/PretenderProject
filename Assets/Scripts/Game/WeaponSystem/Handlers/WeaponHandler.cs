using UnityEngine;

namespace Game.WeaponSystem.Handlers
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _weapon; 
            
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