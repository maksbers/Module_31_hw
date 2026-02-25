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

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _activeEnemies = new ObservableList<CharacterEntity>();

            SpawnPlayer();

            _gameManager.Initialize(_activeEnemies, _player);
            _enemiesSpawner.Initialize(_activeEnemies);

            _gameManager.GameEnded += StopSpawning;
            _gameManager.GameWon += StopSpawning;
        }

        private void StopSpawning()
        {
            if (_enemiesSpawner != null)
                _enemiesSpawner.StopSpawning();
        }

        private void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.GameEnded -= StopSpawning;
                _gameManager.GameWon -= StopSpawning;
            }
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
