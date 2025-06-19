using System;
using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Boss {
    public class BossCircleAttack : BossAttack {
        public override float TriggerDistance => 4f;
        public override AttackType AttackType => AttackType.Melee;
        public override float AttackDuration => 3f;
        public override int Damage => 20;

        public override string AnimatorAttackName {
            get => "MeleeAttack";
            set { }
        }

        [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;
        [SerializeField] private float spacing = 3f;


        private void Start() {
            foreach (BossDamageApplier damageApplier in attackDamageApplierList) {
                damageApplier.Attack = this;
            }
        }

        public override bool TryAttack() {
            if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
                // przekazac info do animatora. Odpalic korutyne czekajaca na koniec animacji. Zeby powrocila bossowi stan walking. 
                // zinstantietowac damageAppliery
                InvokeOnBossAttack();
                CreateDamageAppliers();
                return true;
            }

            return false;
        }

        private void CreateDamageAppliers() {
            if (attackDamageApplierList == null || attackDamageApplierList.Count == 0)
                return;

            Vector3 direction = transform.forward.normalized;
            Vector3 startPos = transform.position + direction * spacing;

            for (int i = 0; i < attackDamageApplierList.Count; i++) {
                Vector3 spawnPos = startPos + direction * spacing * i;
                Instantiate(attackDamageApplierList[i].gameObject, spawnPos,
                    transform.rotation); // optional: face same direction
            }
        }
    }
}