using System;
using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Boss {
    public class GameBoss : MonoBehaviour, IDamageable {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public event EventHandler<IDamageable.DamageTakenArgs> OnDamageTaken;
        public event Action OnDeath;

        public BossState BossState { get; set; } = BossState.Walking;
        public float distance;

        public List<Attack> Attacks { get; set; } = new List<Attack>();

        [SerializeField] private int initialHealth;

        private NavMeshAgent _agent;
        private Transform _target;

        private void Awake() {
            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }

        private void Start() {
            this._agent = GetComponent<NavMeshAgent>();
            this._target = Player.Player.Instance.transform;
        }

        private void Update() {
            this.distance = Vector3.Distance(this.transform.position, this._target.position);
            this.HandleBossState();
        }

        private void SetDistance() {
            this.distance = Vector3.Distance(this.transform.position, this._target.position);
        }


        private void HandleBossState() {
            switch (BossState) {
                case BossState.Walking:
                    break;
                case BossState.Attack01:
                    break;
                case BossState.Attack02:
                    break;
                case BossState.Attack03:
                    break;
                case BossState.Dash:
                    break;
                case BossState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TakeDamage(int damage, Vector3 attackerPosition, bool ranged) {
            CurrentHealth -= damage;
            OnDamageTaken?.Invoke(this, new IDamageable.DamageTakenArgs(CurrentHealth, damage));

            if (CurrentHealth <= 0 && this.BossState != BossState.Dead) {
                this.BossState = BossState.Dead;
                OnDeath?.Invoke();
            }
        }
    }
}