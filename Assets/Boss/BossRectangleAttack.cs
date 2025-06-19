using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Boss {
    public class BossRectangleAttack : BossAttack {
        public override float TriggerDistance => 4f;
        public override AttackType AttackType => AttackType.Ranged;
        public override float AttackDuration => 5f;
        public override int Damage => 10;


        public override string AnimatorAttackName {
            get => "RectangleAttack";
            set { }
        }

        [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;
        [SerializeField] private GameObject vfx;


        private void Start() {
            foreach (BossDamageApplier damageApplier in attackDamageApplierList) {
                damageApplier.Attack = this;
            }
        }

        public override bool TryAttack() {
            if (this.IsOnCooldown) return false;
            if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
                InvokeOnBossAttack();
                CreateDamageAppliers();
                LastAttackTime = Time.time;
                return true;
            }

            return false;
        }


        private void CreateDamageAppliers() {
            GameObject instance = Instantiate(attackDamageApplierList[0].gameObject, transform);
            instance.transform.localPosition = Vector3.forward;
            if (instance.TryGetComponent<IDamageApplier>(out var applier)) {
                applier.Attack = this;
            }
        }

        private void OnIndicatorAnimationFinishEvent() {
            GameObject.Instantiate(vfx.gameObject, transform);
        }
    }
}