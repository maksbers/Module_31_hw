using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GhostsGame.Conditions;
using GhostsGame.Utils;
using GhostsGame.Character;

namespace GhostsGame.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Win Conditions")]
        [SerializeField] private List<Configs.GameConditionConfig> _winConditionConfigs = new();

        [Header("Lose Conditions")]
        [SerializeField] private List<Configs.GameConditionConfig> _loseConditionConfigs = new();

        private List<IGameCondition> _activeWinConditions = new();
        private List<IGameCondition> _activeLoseConditions = new();

        private CharacterEntity _player;
        private EnemiesSpawner _enemiesSpawner;
        private bool _isGameOver;

        private ObservableList<CharacterEntity> _activeEnemies;

        public void Initialize(ObservableList<CharacterEntity> activeEnemies, CharacterEntity player, EnemiesSpawner enemiesSpawner)
        {
            _activeEnemies = activeEnemies;
            _player = player;
            _enemiesSpawner = enemiesSpawner;

            SetupWinConditions(activeEnemies, player);
            SetupLoseConditions(activeEnemies, player);

            foreach (IGameCondition condition in _activeWinConditions)
                condition.StartChecking();

            foreach (IGameCondition condition in _activeLoseConditions)
                condition.StartChecking();

            _enemiesSpawner?.Initialize(activeEnemies);
        }

        private void Update()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
                RestartGame();
        }

        private void SetupWinConditions(ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            foreach (Configs.GameConditionConfig config in _winConditionConfigs)
            {
                if (config == null)
                    continue;

                IGameCondition condition = config.CreateCondition(this, activeEnemies, player);

                condition.ConditionMet += Win;
                _activeWinConditions.Add(condition);
            }
        }

        private void SetupLoseConditions(ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            foreach (Configs.GameConditionConfig config in _loseConditionConfigs)
            {
                if (config == null)
                    continue;

                IGameCondition condition = config.CreateCondition(this, activeEnemies, player);

                condition.ConditionMet += GameOver;
                _activeLoseConditions.Add(condition);
            }
        }

        public event Action GameEnded;
        public event Action GameWon;

        private void GameOver()
        {
            if (_isGameOver)
                return;

            _isGameOver = true;

            if (_player != null && _player.Health.CurrentHealth > 0)
                _player.Health.TakeDamage(_player.Health.CurrentHealth);

            StopConditions();
            GameEnded?.Invoke();

            Debug.Log("Game Over! Press R to Restart.");
        }

        private void Win()
        {
            if (_isGameOver)
                return;

            _isGameOver = true;

            StopConditions();
            GameWon?.Invoke();

            Debug.Log("You Win! Press R to Restart.");
        }

        private void StopConditions()
        {
            if (_player != null)
                _player.SetActive(false);

            if (_activeEnemies != null)
            {
                foreach (CharacterEntity enemy in _activeEnemies.Items)
                {
                    if (enemy != null)
                        enemy.SetActive(false);
                }
            }

            foreach (IGameCondition condition in _activeWinConditions)
                condition.StopChecking();

            foreach (IGameCondition condition in _activeLoseConditions)
                condition.StopChecking();

            _enemiesSpawner?.StopSpawning();
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
