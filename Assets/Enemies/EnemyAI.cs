using System;
using System.Collections.Generic;
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
        public event EventHandler OnAttack;
        public event EventHandler<IDamageable.DamageTakenArgs> OnDamageTaken;
        public float distance;


        public List<Attack> Attacks { get; set; } = new List<Attack>();
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        [SerializeField] private Attack attack;
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
            this._target = Player.Player.Instance.transform;
        }

        void Update() {
            this.distance = Vector3.Distance(this.transform.position, this._target.position);

            switch (this.State) {
                case EnemyState.Walking:

                    _agent.isStopped = false;
                    _agent.SetDestination(this._target.position);
                    if (attack.TryAttack()) {
                        this.State = EnemyState.Attacking;

                        _agent.isStopped = true;
                        _attackTimer = attack.AttackDuration;


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
            ((IDamageable)this).TakeDamage(damage);
            OnDamageTaken?.Invoke(this, new IDamageable.DamageTakenArgs(CurrentHealth, damage));
            Debug.Log($"Enemy took {damage} damage");
        }

        public Attack GetEnemyAttack() {
            return this.attack;
        }
    }
}