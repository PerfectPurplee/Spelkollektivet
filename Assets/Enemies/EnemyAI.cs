using System;
using Interface;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies {
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour, IDamageable {
        public enum EnemyState {
            Walking,
            Attacking
        }

        public EnemyState State { get; set; } = EnemyState.Walking;
        public EventHandler OnAttack;
        public EventHandler OnTakeDamage;
        public float distance;


        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private int initialHealth;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private NavMeshAgent _agent;
        private Transform _target;
        private float _attackTimer = 0;


        private void Awake() {
            this.CurrentHealth = this.MaxHealth = this.initialHealth;
        }

        void Start() {
            this._agent = GetComponent<NavMeshAgent>();
            this._target = PlayerPos.Instance.transform;
        }

        void Update() {
            this.distance = Vector3.Distance(this.transform.position, this._target.position);

            switch (this.State) {
                case EnemyState.Walking:

                    _agent.isStopped = false;
                    _agent.SetDestination(this._target.position);
                    if (enemyAttack.TryAttack()) {
                        this.State = EnemyState.Attacking;

                        _agent.isStopped = true;
                        _attackTimer = enemyAttack.AttackDuration;


                        OnAttack?.Invoke(this, EventArgs.Empty);
                        Debug.Log("enemy attacking");
                    }

                    break;
                case EnemyState.Attacking:
                    _attackTimer -= Time.deltaTime;
                    if (_attackTimer <= 0) {
                        this.State = EnemyState.Walking;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TakeDamage(int damage) {
            CurrentHealth -= damage;
            OnTakeDamage?.Invoke(this, EventArgs.Empty);
            Debug.Log($"Enemy took {damage} damage");
        }

        public EnemyAttack GetEnemyAttack() {
            return this.enemyAttack;
        }
    }
}