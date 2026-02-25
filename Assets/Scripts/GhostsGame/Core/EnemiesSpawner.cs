using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GhostsGame.Utils;
using GhostsGame.Character;

namespace GhostsGame.Core
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private CharactersFactory _charactersFactory;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _spawnCooldown = 2f;

        private ObservableList<CharacterEntity> _activeEnemies;
        private bool _isSpawning;

        public void Initialize(ObservableList<CharacterEntity> activeEnemies)
        {
            _activeEnemies = activeEnemies;
            _isSpawning = true;

            StartCoroutine(SpawnRoutine());
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private IEnumerator SpawnRoutine()
        {
            while (_isSpawning)
            {
                if (_spawnPoints.Count > 0)
                {
                    Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

                    CharacterEntity newEnemy = _charactersFactory.CreateEnemy(spawnPoint.position, spawnPoint.rotation);

                    if (newEnemy != null)
                    {
                        _activeEnemies.Add(newEnemy);

                        newEnemy.Health.Died += () => RemoveEnemy(newEnemy);
                    }
                }

                yield return new WaitForSeconds(_spawnCooldown);
            }
        }

        private void RemoveEnemy(CharacterEntity enemy)
        {
            _activeEnemies.Remove(enemy);
        }
    }
}
