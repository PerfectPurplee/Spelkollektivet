using System;
using UnityEngine;

namespace Interface {
    public interface IDamageable {
        public class DamageTakenArgs : EventArgs {
            public int CurrentHealth { get; set; }
            public int Damage { get; set; }

            public DamageTakenArgs(int currentHealth, int damage) {
                CurrentHealth = currentHealth;
                Damage = damage;
            }
        }

        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        event EventHandler<DamageTakenArgs> OnDamageTaken;
        event Action OnDeath;

        void TakeDamage(int damage, Vector3 attackerPosition, bool ranged);
    }
}