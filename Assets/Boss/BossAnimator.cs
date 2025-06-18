using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Boss {
    public class BossAnimator : MonoBehaviour {
        [SerializeField] private Animator animator;

        [SerializeField] private List<BossAttack> bossAttackList;

        public event EventHandler OnAttackAnimationFinished;

        private void Start() {
            foreach (var bossAttack in bossAttackList) {
                bossAttack.OnBossAttack += HandleBossAttackEvent;
            }
        }

        private void HandleBossAttackEvent(object sender, BossAttack.BossAttackArgs attackArgs) {
            animator.SetTrigger(attackArgs.AttackName);
            StartCoroutine(WaitForAnimationFinishAndDestroy(attackArgs.AttackName));
        }

        private IEnumerator WaitForAnimationFinishAndDestroy(string attackName) {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(attackName)) {
                yield return null;
            }

            var stateinfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateinfo.length);
            OnAttackAnimationFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}