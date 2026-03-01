using UnityEngine;
using GhostsGame.Utils;
using GhostsGame.Character;

namespace GhostsGame.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private CharactersFactory _charactersFactory;
        [SerializeField] private EnemiesSpawner _enemiesSpawner;
        [SerializeField] private Transform _playerSpawnPoint;

        private ObservableList<CharacterEntity> _activeEnemies;
        private CharacterEntity _player;
        private ControllersUpdateService _controllersUpdateService;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _controllersUpdateService = new ControllersUpdateService();
            _charactersFactory.Initialize(_controllersUpdateService);

            _activeEnemies = new ObservableList<CharacterEntity>();

            SpawnPlayer();

            _gameManager.Initialize(_activeEnemies, _player, _enemiesSpawner);
        }

        private void Update()
        {
            _controllersUpdateService?.Update();
        }

        private void SpawnPlayer()
        {
            if (_playerSpawnPoint == null)
            {
                Debug.LogError("Bootstrap: Player spawn point is not set!");
                return;
            }

            _player = _charactersFactory.CreatePlayer(_playerSpawnPoint.position, _playerSpawnPoint.rotation);
        }
    }
}
