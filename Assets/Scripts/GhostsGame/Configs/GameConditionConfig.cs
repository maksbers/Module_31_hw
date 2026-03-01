using UnityEngine;
using GhostsGame.Conditions;
using GhostsGame.Character;
using GhostsGame.Core;
using GhostsGame.Utils;

namespace GhostsGame.Configs
{
    public abstract class GameConditionConfig : ScriptableObject
    {
        public abstract IGameCondition CreateCondition(GameManager context, ObservableList<CharacterEntity> activeEnemies, CharacterEntity player);
    }
}
