using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Boss {
    public class BossRectangleAttack : BossAttack {
        public override float TriggerDistance => 6f;
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
            GameObject instance = Instantiate(attackDamageApplierList[0].gameObject, transform);
            instance.transform.localPosition = Vector3.forward;

        }
    }
}