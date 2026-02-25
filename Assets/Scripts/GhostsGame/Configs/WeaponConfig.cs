using UnityEngine;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public BulletConfig BulletType { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; } = 0.2f;
    }
}
