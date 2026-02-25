using System;

namespace GhostsGame.Conditions
{
    public class PlayerDeathCondition : IGameCondition
    {
        public event Action ConditionMet;
        private readonly Character.CharacterEntity _player;

        public PlayerDeathCondition(Character.CharacterEntity player)
        {
            _player = player;
        }

        public void StartChecking()
        {
            if (_player != null)
                _player.Health.Died += OnPlayerDied;
        }

        public void StopChecking()
        {
            if (_player != null)
                _player.Health.Died -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            ConditionMet?.Invoke();
            StopChecking();
        }
    }
}
