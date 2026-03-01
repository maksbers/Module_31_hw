using UnityEngine;
using GhostsGame.Character;
using GhostsGame.Controllers;
using GhostsGame.Configs;

namespace GhostsGame.Core
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfig _enemyConfig;

        private ControllersUpdateService _controllersUpdateService;

        public void Initialize(ControllersUpdateService controllersUpdateService)
        {
            _controllersUpdateService = controllersUpdateService;
        }

        public CharacterEntity CreatePlayer(Vector3 position, Quaternion rotation)
        {
            if (_playerConfig == null || _playerConfig.Prefab == null)
            {
                Debug.LogError("CharactersFactory: Player Config or Prefab is not assigned!");
                return null;
            }

            CharacterEntity player = Instantiate(_playerConfig.Prefab, position, rotation);
            WeaponSystem weapon = new WeaponSystem(_playerConfig.WeaponConfig, player.FirePoint);

            player.Initialize(
                _playerConfig.MoveSpeed,
                _playerConfig.RotationSpeed,
                _playerConfig.MaxHealth,
                _playerConfig.DeathDelay,
                weapon
            );

            PlayerController controller = new PlayerController(player);

            if (_controllersUpdateService != null)
            {
                _controllersUpdateService.Register(controller);
                player.Health.Died += () => _controllersUpdateService.Unregister(controller);
            }

            return player;
        }

        public CharacterEntity CreateEnemy(Vector3 position, Quaternion rotation)
        {
            if (_enemyConfig == null || _enemyConfig.Prefab == null)
            {
                Debug.LogError("CharactersFactory: Enemy Config or Prefab is not assigned!");
                return null;
            }

            CharacterEntity enemy = Instantiate(_enemyConfig.Prefab, position, rotation);
            EmptyWeapon weapon = new EmptyWeapon();

            enemy.Initialize(
                _enemyConfig.MoveSpeed,
                _enemyConfig.RotationSpeed,
                _enemyConfig.MaxHealth,
                _enemyConfig.DeathDelay,
                weapon
            );

            EnemyAIController controller = new EnemyAIController(enemy, _enemyConfig);

            if (_controllersUpdateService != null)
            {
                _controllersUpdateService.Register(controller);
                enemy.Health.Died += () => _controllersUpdateService.Unregister(controller);
            }

            return enemy;
        }
    }
}
