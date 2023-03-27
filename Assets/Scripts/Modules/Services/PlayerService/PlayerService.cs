using UnityEngine;

namespace Modules.Services.PlayerService
{
    public class PlayerService : Singleton<PlayerService>
    {
        [SerializeField] private GameObject _player;

        public GameObject Player => _player;
    }
}