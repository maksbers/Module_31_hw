using UnityEngine;
using GhostsGame.Character;
using GhostsGame.Configs;

namespace GhostsGame.Controllers
{
    public class EnemyAIController : IEntityController
    {
        private readonly CharacterEntity _character;
        private readonly float _directionChangeInterval;
        private readonly int _damage;

        private Vector3 _currentDirection;
        private float _directionTimer;

        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");

        private bool _hasAttacked;

        public EnemyAIController(CharacterEntity character, EnemyConfig config)
        {
            _character = character;
            _directionChangeInterval = config.DirectionChangeInterval;
            _damage = config.Damage;

            _character.Hit += OnControllerColliderHit;
            _character.Health.Died += OnDeath;

            ChooseRandomDirection();
        }

        public void OnUpdate()
        {
            if (_hasAttacked || _character == null || !_character.IsActive)
            {
                if (_character != null && _character.Animator != null)
                    _character.Animator.SetBool(IsRunningKey, false);

                return;
            }

            if (_character.Animator != null)
                _character.Animator.SetBool(IsRunningKey, true);

            _directionTimer -= Time.deltaTime;

            if (_directionTimer <= 0)
                ChooseRandomDirection();

            _character.Mover?.MoveTo(_currentDirection);
            _character.Rotator?.RotateTo(_currentDirection);
        }

        private void ChooseRandomDirection()
        {
            float randomAngle = Random.Range(0f, 360f);
            _currentDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;

            _directionTimer = _directionChangeInterval;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_hasAttacked)
                return;

            if (hit.gameObject.TryGetComponent(out CharacterEntity otherEntity))
                HandleImpactWithPlayer(otherEntity);
        }

        private void HandleImpactWithPlayer(CharacterEntity otherEntity)
        {
            if (otherEntity.Type == EntityType.Player && otherEntity.Health != null)
            {
                _hasAttacked = true;

                otherEntity.Health.TakeDamage(_damage);

                if (_character.Health != null)
                    _character.Health.TakeDamage(_character.Health.CurrentHealth);
            }
        }

        private void OnDeath()
        {
            if (_character != null && _character.Animator != null)
                _character.Animator.SetBool(IsRunningKey, false);
        }
    }
}
