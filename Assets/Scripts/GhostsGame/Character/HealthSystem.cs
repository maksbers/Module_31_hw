using System;

namespace GhostsGame.Character
{
    public class HealthSystem
    {
        public event Action Died;
        public event Action<int> HealthChanged;

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public HealthSystem(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            CurrentHealth -= damage;
            HealthChanged?.Invoke(CurrentHealth);

            if (IsDead)
                Died?.Invoke();
        }
    }
}
