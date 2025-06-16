using System;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies {
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour {
        public enum EnemyState {
            Walking,
            Attacking
        }

        public EnemyState State { get; set; } = EnemyState.Walking;
        public EventHandler OnAttack;
        public float distance;

        [SerializeField] private EnemyAttack enemyAttack;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private NavMeshAgent _agent;
        private Animator _animator;
        private Transform _target;
        private float _attackTimer = 0;

        void Start() {
            this._agent = GetComponent<NavMeshAgent>();
            this._animator = GetComponent<Animator>();
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
    }
}