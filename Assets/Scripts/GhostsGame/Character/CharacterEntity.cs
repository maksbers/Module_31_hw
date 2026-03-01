using UnityEngine;
using System;

namespace GhostsGame.Character
{
    public enum EntityType
    {
        Player,
        Enemy
    }

    public class CharacterEntity : MonoBehaviour
    {
        [field: SerializeField] public EntityType Type { get; private set; }

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Animator _animator;

        public Mover Mover { get; private set; }
        public Rotator Rotator { get; private set; }
        public HealthSystem Health { get; private set; }
        public IWeapon Weapon { get; private set; }
        public Animator Animator => _animator;
        public Transform FirePoint => _firePoint;

        public event Action<ControllerColliderHit> Hit;

        private bool _isInitialized;
        private bool _isDying;
        public bool IsActive { get; private set; } = true;
        private float _deathDelay;

        public void Initialize(float moveSpeed, float rotationSpeed, int maxHealth, float deathDelay, IWeapon weapon)
        {
            if (_characterController == null)
                _characterController = GetComponent<CharacterController>();

            Mover = new Mover(_characterController, moveSpeed);
            Rotator = new Rotator(transform, rotationSpeed);
            Health = new HealthSystem(maxHealth);
            _deathDelay = deathDelay;
            Weapon = weapon;

            Health.Died += OnDeath;
            _isInitialized = true;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (!isActive && _animator != null)
                _animator.SetBool("IsRunning", false);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_isInitialized || _isDying || !IsActive)
                return;

            Hit?.Invoke(hit);
        }

        private void OnDeath()
        {
            _isDying = true;

            if (_characterController != null)
                _characterController.enabled = false;

            Destroy(gameObject, _deathDelay);
        }

        private void OnDestroy()
        {
            if (Health != null)
                Health.Died -= OnDeath;
        }
    }
}
