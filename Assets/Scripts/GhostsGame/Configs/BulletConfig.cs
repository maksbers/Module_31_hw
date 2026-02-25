using UnityEngine;
using GhostsGame.Weapons;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/BulletConfig", fileName = "BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public Bullet Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        [field: SerializeField] public float Lifetime { get; private set; } = 3f;
        [field: SerializeField] public int Damage { get; private set; } = 1;
    }
}
