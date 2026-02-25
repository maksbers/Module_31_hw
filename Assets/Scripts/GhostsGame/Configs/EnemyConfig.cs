using UnityEngine;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : EntityConfig
    {
        [field: SerializeField] public float DirectionChangeInterval { get; private set; } = 3f;
        [field: SerializeField] public int DamageToPlayer { get; private set; } = 1;
    }
}
