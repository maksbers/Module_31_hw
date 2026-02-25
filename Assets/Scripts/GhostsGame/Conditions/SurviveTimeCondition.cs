using System;
using UnityEngine;

namespace GhostsGame.Conditions
{
    public class SurviveTimeCondition : IGameCondition
    {
        public event Action ConditionMet;

        private readonly float _requiredTime;
        private float _currentTime;
        private bool _isChecking;
        private readonly MonoBehaviour _coroutineRunner;

        public SurviveTimeCondition(float requiredTime, MonoBehaviour coroutineRunner)
        {
            _requiredTime = requiredTime;
            _coroutineRunner = coroutineRunner;
        }

        public void StartChecking()
        {
            _isChecking = true;
            _currentTime = 0f;
            _coroutineRunner.StartCoroutine(CheckRoutine());
        }

        public void StopChecking()
        {
            _isChecking = false;
        }

        private System.Collections.IEnumerator CheckRoutine()
        {
            while (_isChecking)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _requiredTime)
                {
                    ConditionMet?.Invoke();
                    StopChecking();
                }

                yield return null;
            }
        }
    }
}
