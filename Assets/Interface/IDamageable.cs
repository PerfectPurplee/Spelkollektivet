using System;
using UnityEngine;

namespace Interface {
    public interface IDamageable {
        public class DamageTakenArgs : EventArgs {
            private int CurrentHealth { get; set; }
            private int Damage { get; set; }

            public DamageTakenArgs(int currentHealth, int damage) {
                CurrentHealth = currentHealth;
                Damage = damage;
            }
        }

        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        event EventHandler<DamageTakenArgs> OnDamageTaken;

        void TakeDamage(int damage) {
            CurrentHealth -= damage;
        }
    }
}