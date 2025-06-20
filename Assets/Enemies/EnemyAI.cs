using System;
using System.Collections;
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
            Attacking,
            Dying
        }

        public EnemyState State { get; set; } = EnemyState.Walking;
        public event EventHandler OnAttack;
        public event EventHandler<IDamageable.DamageTakenArgs> OnDamageTaken;
        public float distance;
        public event Action OnDeath;
        public static event Action StaticOnDamage;
        public static event Action StaticOnDeath;

        public List<Attack> Attacks { get; set; } = new List<Attack>();
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        [SerializeField] private Attack attack;
        [SerializeField] private int initialHealth;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private NavMeshAgent _agent;
        private Transform _target;
        private float _attackTimer = 0;

        [SerializeField]
        private Color hitMaterialColor;
        [SerializeField]
        private float hitFadeTime;
        [SerializeField]
        private SkinnedMeshRenderer meshRenderer;
        private float lastHitTime = -1;
        private Color currentColor;

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

                    _agent.SetDestination(this._target.position);
                    if (attack.TryAttack()) {
                        this.State = EnemyState.Attacking;

                        _agent.isStopped = true;
                        _agent.ResetPath();
                        _attackTimer = attack.AttackDuration;


                        OnAttack?.Invoke(this, EventArgs.Empty);
                        //Debug.Log("enemy attacking");
                    }

                    break;
                case EnemyState.Attacking:
                    _attackTimer -= Time.deltaTime;
                    if (_attackTimer <= 0) {
                        if (this.attack is EnemyBasicMeleeAttack meleeAttack) {
                            meleeAttack.TurnWeaponColliderOffAfterMeleeAttack();
                        }

                        _agent.isStopped = false;
                        this.State = EnemyState.Walking;
                    }

                    break;
                case EnemyState.Dying:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (meshRenderer != null)
            {
                if (Time.time - lastHitTime > hitFadeTime)
                {
                    if (currentColor != Color.white)
                    {
                        meshRenderer.material.color = Color.white;
                    }
                }
                else
                {
                    currentColor = Color.Lerp(hitMaterialColor, Color.white, (Time.time - lastHitTime) / hitFadeTime);
                    meshRenderer.material.color = currentColor;
                }
            }
        }

        public void TakeDamage(int damage, Vector3 attackerPosition, bool range) {
            CurrentHealth -= damage;
            OnDamageTaken?.Invoke(this, new IDamageable.DamageTakenArgs(CurrentHealth, damage));
            lastHitTime = Time.time;
            StaticOnDamage?.Invoke();

            if (CurrentHealth <= 0 && State != EnemyState.Dying) {
                StaticOnDeath?.Invoke();
                State = EnemyState.Dying;
                OnDeath?.Invoke();
            }

            //Debug.Log($"Enemy took {damage} damage");
        }

        public Attack GetEnemyAttack() {
            return this.attack;
        }
    }
}