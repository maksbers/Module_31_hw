using System;
using GhostsGame.Utils;
using GhostsGame.Character;

namespace GhostsGame.Conditions
{
    public class EnemyOverrunCondition : IGameCondition
    {
        public event Action ConditionMet;

        private readonly int _maxEnemies;
        private readonly ObservableList<CharacterEntity> _activeEnemies;

        public EnemyOverrunCondition(int maxEnemies, ObservableList<CharacterEntity> activeEnemies)
        {
            _maxEnemies = maxEnemies;
            _activeEnemies = activeEnemies;
        }

        public void StartChecking()
        {
            _activeEnemies.ItemAdded += OnEnemyAdded;
        }

        public void StopChecking()
        {
            _activeEnemies.ItemAdded -= OnEnemyAdded;
        }

        private void OnEnemyAdded(CharacterEntity enemy)
        {
            if (_activeEnemies.Count > _maxEnemies)
            {
                ConditionMet?.Invoke();
                StopChecking();
            }
        }
    }
}
