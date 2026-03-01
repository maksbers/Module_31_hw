using UnityEngine;
using GhostsGame.Conditions;
using GhostsGame.Character;
using GhostsGame.Core;
using GhostsGame.Utils;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/Conditions/KillCount", fileName = "KillCountCondition")]
    public class KillCountConditionConfig : GameConditionConfig
    {
        [SerializeField] private int _requiredKills = 10;

        public override IGameCondition CreateCondition(GameManager context, ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            return new KillCountCondition(_requiredKills, activeEnemies);
        }
    }
}
