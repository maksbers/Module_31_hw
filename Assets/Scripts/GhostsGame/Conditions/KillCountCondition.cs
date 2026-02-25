using System;
using GhostsGame.Utils;
using GhostsGame.Character;

namespace GhostsGame.Conditions
{
    public class KillCountCondition : IGameCondition
    {
        public event Action ConditionMet;

        private readonly int _requiredKills;
        private readonly ObservableList<CharacterEntity> _activeEnemies;
        private int _currentKills;

        public KillCountCondition(int requiredKills, ObservableList<CharacterEntity> activeEnemies)
        {
            _requiredKills = requiredKills;
            _activeEnemies = activeEnemies;
        }

        public void StartChecking()
        {
            _activeEnemies.ItemRemoved += OnEnemyRemoved;
        }

        public void StopChecking()
        {
            _activeEnemies.ItemRemoved -= OnEnemyRemoved;
        }

        private void OnEnemyRemoved(CharacterEntity enemy)
        {
            _currentKills++;

            if (_currentKills >= _requiredKills)
            {
                ConditionMet?.Invoke();
                StopChecking();
            }
        }
    }
}
