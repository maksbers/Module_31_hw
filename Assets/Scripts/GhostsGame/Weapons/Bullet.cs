using UnityEngine;
using GhostsGame.Character;

namespace GhostsGame.Weapons
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _lifetime;
        private int _damage;

        public void Initialize(Configs.BulletConfig config)
        {
            _speed = config.Speed;
            _lifetime = config.Lifetime;
            _damage = config.Damage;

            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterEntity enemy) && enemy.Type == EntityType.Enemy)
            {
                enemy.Health.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
