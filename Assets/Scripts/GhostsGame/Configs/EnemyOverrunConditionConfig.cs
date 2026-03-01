using UnityEngine;
using GhostsGame.Conditions;
using GhostsGame.Character;
using GhostsGame.Core;
using GhostsGame.Utils;

namespace GhostsGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostsGame/Conditions/EnemyOverrun", fileName = "EnemyOverrunCondition")]
    public class EnemyOverrunConditionConfig : GameConditionConfig
    {
        [SerializeField] private int _maxEnemies = 20;

        public override IGameCondition CreateCondition(GameManager context, ObservableList<CharacterEntity> activeEnemies, CharacterEntity player)
        {
            return new EnemyOverrunCondition(_maxEnemies, activeEnemies);
        }
    }
}
