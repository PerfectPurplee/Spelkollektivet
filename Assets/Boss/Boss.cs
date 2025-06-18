using System;
using System.Collections.Generic;
using System.Linq;
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
        public float distanceToPlayer;

        [field: SerializeField] public List<Attack> AttackList { get; private set; }
        [SerializeField] private int initialHealth;
        [SerializeField] private BossAnimator bossAnimator;

        private NavMeshAgent _agent;
        private Transform _target;

        private void Awake() {
            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }

        private void Start() {
            this._agent = GetComponent<NavMeshAgent>();
            this._target = Player.Player.Instance.transform;
            this.bossAnimator.OnAttackAnimationFinished += OnAttackAnimationFinished;
        }


        private void Update() {
            this.UpdateDistanceToPlayer();
            this.HandleBossState();
        }

        private void UpdateDistanceToPlayer() {
            this.distanceToPlayer = Vector3.Distance(this.transform.position, this._target.position);
        }


        private void HandleBossState() {
            switch (BossState) {
                case BossState.Walking:
                    UpdateAgent(true);

                    if (AttackCheck()) {
                        BossState = BossState.Attacking;
                    }

                    break;
                case BossState.Attacking:
                    UpdateAgent(false);
                    break;
                case BossState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool AttackCheck() {
            AttackList.Shuffle();
            return AttackList.Any(attack => attack.TryAttack());
        }

        private void UpdateAgent(bool isActive) {
            if (isActive) {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
            }
            else {
                _agent.isStopped = true;
                _agent.ResetPath();
            }
        }

        private void OnAttackAnimationFinished(object sender, EventArgs e) {
            this.BossState = BossState.Walking;
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