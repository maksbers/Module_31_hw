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
        public event Action GameEnded;
        public event Action GameWon;

        [Header("Win Conditions")]
        [SerializeField] private bool _winBySurviveTime = true;
        [SerializeField] private float _surviveTime = 60f;
        [Space]
        [SerializeField] private bool _winByKillCount = false;
        [SerializeField] private int _enemiesToKill = 10;

        [Header("Lose Conditions")]
        [SerializeField] private bool _loseByPlayerDeath = true;
        [Space]
        [SerializeField] private bool _loseByEnemyOverrun = false;
        [SerializeField] private int _maxEnemiesOnArena = 20;

        private List<IGameCondition> _activeWinConditions = new();
        private List<IGameCondition> _activeLoseConditions = new();

        private CharacterEntity _player;
        private bool _isGameOver;


        public void Initialize(ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            _player = player;

            SetupWinConditions(activeEnemies);
            SetupLoseConditions(activeEnemies, player);

            foreach (IGameCondition condition in _activeWinConditions)
                condition.StartChecking();

            foreach (IGameCondition condition in _activeLoseConditions)
                condition.StartChecking();
        }

        private void Update()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
                RestartGame();
        }

        private void SetupWinConditions(ObservableList<CharacterEntity> activeEnemies)
        {
            if (_winBySurviveTime)
            {
                SurviveTimeCondition condition = new SurviveTimeCondition(_surviveTime, this);
                condition.ConditionMet += Win;

                _activeWinConditions.Add(condition);
            }

            if (_winByKillCount)
            {
                KillCountCondition condition = new KillCountCondition(_enemiesToKill, activeEnemies);
                condition.ConditionMet += Win;

                _activeWinConditions.Add(condition);
            }
        }

        private void SetupLoseConditions(ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            if (_loseByPlayerDeath)
            {
                PlayerDeathCondition condition = new PlayerDeathCondition(player);
                condition.ConditionMet += GameOver;

                _activeLoseConditions.Add(condition);
            }

            if (_loseByEnemyOverrun)
            {
                EnemyOverrunCondition condition = new EnemyOverrunCondition(_maxEnemiesOnArena, activeEnemies);
                condition.ConditionMet += GameOver;

                _activeLoseConditions.Add(condition);
            }
        }



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

            foreach (IGameCondition condition in _activeWinConditions)
                condition.StopChecking();

            foreach (IGameCondition condition in _activeLoseConditions)
                condition.StopChecking();
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
