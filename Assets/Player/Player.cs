using System;
using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour, IDamageable {
        public static Player Instance { get; private set; }
        public event EventHandler<IDamageable.DamageTakenArgs> OnDamageTaken;
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public List<Attack> Attacks { get; set; }

        [SerializeField] private int initialHealth;

        private void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }


        public void TakeDamage(int damage) {
            CurrentHealth -= damage;
            OnDamageTaken?.Invoke(this, new IDamageable.DamageTakenArgs(CurrentHealth, damage));
            Debug.Log($"Player took {damage} damage, current health: {CurrentHealth}");
        }
    }
}