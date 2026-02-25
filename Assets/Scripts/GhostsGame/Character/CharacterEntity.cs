using UnityEngine;
using GhostsGame.Controllers;
using GhostsGame.Weapons;

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
        public WeaponSystem Weapon { get; private set; }
        public Animator Animator => _animator;

        private IEntityController _controller;
        private bool _isInitialized;
        private bool _isDying;
        private bool _isActive = true;
        private float _deathDelay;

        private void InitializeCore(Configs.EntityConfig config, IEntityController controller)
        {
            if (_characterController == null)
                _characterController = GetComponent<CharacterController>();

            Mover = new Mover(_characterController, config.MoveSpeed);
            Rotator = new Rotator(transform, config.RotationSpeed);
            Health = new HealthSystem(config.MaxHealth);
            _deathDelay = config.DeathDelay;

            _controller = controller;

            Health.Died += OnDeath;
            _isInitialized = true;
        }

        public void Initialize(Configs.PlayerConfig config, IEntityController controller)
        {
            InitializeCore(config, controller);
            Weapon = new WeaponSystem(config.WeaponConfig, _firePoint);
        }

        public void Initialize(Configs.EnemyConfig config, IEntityController controller)
        {
            InitializeCore(config, controller);
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;

            if (!isActive && _animator != null)
                _animator.SetBool("IsRunning", false);
        }

        private void Update()
        {
            if (!_isInitialized || _isDying || !_isActive)
                return;

            _controller?.OnUpdate();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_isInitialized || _isDying || !_isActive)
                return;

            _controller?.OnControllerColliderHit(hit);
        }

        private void OnDeath()
        {
            _isDying = true;

            if (_characterController != null)
                _characterController.enabled = false;

            _controller?.OnDeath();

            Destroy(gameObject, _deathDelay);
        }

        private void OnDestroy()
        {
            if (Health != null)
                Health.Died -= OnDeath;
        }
    }
}
