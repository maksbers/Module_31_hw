using UnityEngine;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : EntityConfig
    {
        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
    }
}
