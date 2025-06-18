using System;
using System.Collections;
using UnityEngine;

namespace Enemies {
    public class EnemyAnimator : MonoBehaviour {
        private EnemyAI _enemyAI;
        private Animator _animator;


        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");

        private void Awake() {
            _enemyAI = GetComponent<EnemyAI>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() {
            _enemyAI.OnAttack += HandleAttack;
            _enemyAI.OnDeath += EnemyAIOnOnDeath;
        }


        private void OnDisable() {
            _enemyAI.OnAttack -= HandleAttack;
        }


        private void HandleAttack(object sender, EventArgs e) {
            this._animator.SetTrigger(Attack);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void EnemyAIOnOnDeath() {
            this._animator.SetTrigger(Death);
            StartCoroutine(WaitForAnimationFinishAndDestroy());
        }

        private IEnumerator WaitForAnimationFinishAndDestroy() {
            yield return null;

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
                yield return null;
            }

            var stateinfo = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateinfo.length);
            Debug.Log("Death animation finished");

            ExpDropManager.Instance.SpawnExpDrop(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}