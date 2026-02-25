using UnityEngine;
using GhostsGame.Character;

namespace GhostsGame.Configs
{
    public abstract class EntityConfig : ScriptableObject
    {
        [field: SerializeField] public CharacterEntity Prefab { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; } = 1;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
        [field: SerializeField] public float DeathDelay { get; private set; } = 2f;
    }
}
