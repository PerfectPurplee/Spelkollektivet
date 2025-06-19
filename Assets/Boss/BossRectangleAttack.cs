using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Boss {
    public class BossRectangleAttack : BossAttack {
        public override float TriggerDistance => 1f;
        public override AttackType AttackType => AttackType.Ranged;
        public override float AttackDuration => 5f;
        public override int Damage => 20;


        public override string AnimatorAttackName {
            get => "RectangleAttack";
            set { }
        }

        [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;


        private void Start() {
            foreach (BossDamageApplier damageApplier in attackDamageApplierList) {
                damageApplier.Attack = this;
            }
        }

        public override bool TryAttack() {
            if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
                InvokeOnBossAttack();
                CreateDamageAppliers();
                return true;
            }

            return false;
        }


        public void CreateDamageAppliers() {
            Vector3 direction = transform.forward.normalized;
            Vector3 startPos = transform.position + (direction);

            Instantiate(attackDamageApplierList[0].gameObject, startPos, Quaternion.identity);
        }
    }
}