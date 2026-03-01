using UnityEngine;
using GhostsGame.Conditions;
using GhostsGame.Character;
using GhostsGame.Core;
using GhostsGame.Utils;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/Conditions/SurviveTime", fileName = "SurviveTimeCondition")]
    public class SurviveTimeConditionConfig : GameConditionConfig
    {
        [SerializeField] private float _surviveTime = 60f;

        public override IGameCondition CreateCondition(GameManager context, ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            return new SurviveTimeCondition(_surviveTime, context);
        }
    }
}
