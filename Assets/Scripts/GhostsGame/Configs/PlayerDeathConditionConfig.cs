using UnityEngine;
using GhostsGame.Conditions;
using GhostsGame.Character;
using GhostsGame.Core;
using GhostsGame.Utils;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/Conditions/PlayerDeath", fileName = "PlayerDeathCondition")]
    public class PlayerDeathConditionConfig : GameConditionConfig
    {
        public override IGameCondition CreateCondition(GameManager context, ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            return new PlayerDeathCondition(player);
        }
    }
}
