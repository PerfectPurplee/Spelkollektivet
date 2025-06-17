using Interface;
using UnityEngine;

namespace Player {
    public class PlayerPos : MonoBehaviour, IDamageable {
        public static PlayerPos Instance { get; private set; }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        [SerializeField] int initialHealth;

        private void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }


        public void TakeDamage(int damage) {
            CurrentHealth -= damage;
            Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");
        }
    }
}