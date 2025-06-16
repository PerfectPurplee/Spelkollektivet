using System;
using UnityEngine;

namespace Enemies {
    public class EnemyAnimator : MonoBehaviour {
        private EnemyAI _enemyAI;
        private Animator _animator;


        private static readonly int Attack = Animator.StringToHash("Attack");

        private void Awake() {
            _enemyAI = GetComponent<EnemyAI>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() {
            _enemyAI.OnAttack += HandleAttack;
        }

        private void OnDisable() {
            _enemyAI.OnAttack -= HandleAttack;
        }


        private void HandleAttack(object sender, EventArgs e) {
            this._animator.SetTrigger(Attack);
        }
    }
}