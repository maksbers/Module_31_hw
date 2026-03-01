using UnityEngine;
using GhostsGame.Weapons;
using GhostsGame.Configs;

namespace GhostsGame.Character
{
    public class WeaponSystem : IWeapon
    {
        private readonly WeaponConfig _weaponConfig;
        private readonly Transform _firePoint;
        private float _lastFireTime;

        public WeaponSystem(WeaponConfig weaponConfig, Transform firePoint)
        {
            _weaponConfig = weaponConfig;
            _firePoint = firePoint;
        }

        public void Fire()
        {
            if (Time.time - _lastFireTime < _weaponConfig.FireRate)
                return;

            if (_weaponConfig.BulletType == null || _weaponConfig.BulletType.Prefab == null)
            {
                Debug.LogWarning("WeaponSystem: BulletConfig or BulletPrefab is null!");
                return;
            }

            Bullet bullet = Object.Instantiate(_weaponConfig.BulletType.Prefab, _firePoint.position, _firePoint.rotation);

            bullet.Initialize(_weaponConfig.BulletType);

            _lastFireTime = Time.time;
        }
    }
}
