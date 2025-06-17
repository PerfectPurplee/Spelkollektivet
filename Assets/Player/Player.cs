using System;
using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour, IDamageable {
        public static Player Instance { get; private set; }
        public event EventHandler<IDamageable.DamageTakenArgs> OnDamageTaken;
        public event Action OnDeath;
        public event Action OnBlockMelee;
        public event Action OnBlockRanged;

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public List<Attack> Attacks { get; set; }

        [SerializeField] private int initialHealth;

        public bool shielding;
        public Vector3 shieldDirection;
        public float meleeDamageShieldedMultiplier;
        public float shieldingAngle;

        private void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }

        public void TakeDamage(int damage, Vector3 attackerPosition, bool ranged) {
            Vector3 attackVector = (transform.position - attackerPosition).normalized;
            if (shielding && attackVector != Vector3.zero && Vector3.SqrMagnitude(shieldDirection + attackVector) < shieldingAngle)
            {
                if (!ranged)
                {
                    float startDamage = damage;
                    damage = Mathf.CeilToInt(damage * meleeDamageShieldedMultiplier);
                    OnBlockMelee?.Invoke();
                    Debug.Log($"Player blocked melee attack, reducing from {startDamage} to {damage} damage");
                }
                else
                {
                    OnBlockRanged?.Invoke();
                    Debug.Log("Player blocked ranged attack");
                    return;
                }
            }
            CurrentHealth -= damage;
            OnDamageTaken?.Invoke(this, new IDamageable.DamageTakenArgs(CurrentHealth, damage));
            Debug.Log($"Player took {damage} damage, current health: {CurrentHealth}");
        }
    }
}