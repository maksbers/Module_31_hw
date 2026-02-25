using UnityEngine;
using GhostsGame.Character;
using GhostsGame.Controllers;

namespace GhostsGame.Core
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField] private Configs.PlayerConfig _playerConfig;
        [SerializeField] private Configs.EnemyConfig _enemyConfig;

        public CharacterEntity CreatePlayer(Vector3 position, Quaternion rotation)
        {
            if (_playerConfig == null || _playerConfig.Prefab == null)
            {
                Debug.LogError("CharactersFactory: Player Config or Prefab is not assigned!");
                return null;
            }

            CharacterEntity player = Instantiate(_playerConfig.Prefab, position, rotation);
            PlayerController controller = new PlayerController(player);
            player.Initialize(_playerConfig, controller);

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
            EnemyAIController controller = new EnemyAIController(enemy, _enemyConfig);
            enemy.Initialize(_enemyConfig, controller);

            return enemy;
        }
    }
}
