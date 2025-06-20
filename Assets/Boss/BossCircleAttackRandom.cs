using System;
using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boss {
    public class BossCircleAttackRandom : BossAttack {
        public override float TriggerDistance => 20f;
        public override AttackType AttackType => AttackType.Melee;
        public override float AttackDuration => 3f;
        public override int Damage => 40;

        public override string AnimatorAttackName {
            get => "MeleeAttack";
            set { }
        }

        [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;
        [SerializeField] private float spawnRadius = 20f;


        private void Start() {
            foreach (BossDamageApplier damageApplier in attackDamageApplierList) {
                damageApplier.Attack = this;
            }
        }

        public override bool TryAttack() {
            if (this.IsOnCooldown) return false;
            if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
                // przekazac info do animatora. Odpalic korutyne czekajaca na koniec animacji. Zeby powrocila bossowi stan walking. 
                // zinstantietowac damageAppliery
                InvokeOnBossAttack();
                CreateDamageAppliers();
                LastAttackTime = Time.time;
                return true;
            }

            return false;
        }


        private void CreateDamageAppliers() {
            if (attackDamageApplierList == null || attackDamageApplierList.Count == 0)
                return;

            for (int i = 0; i < attackDamageApplierList.Count; i++) {
                Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnOffset = new Vector3(randomCircle.x, 0, randomCircle.y); // flat circle on XZ plane
                Vector3 spawnPos = transform.position + spawnOffset;

                var instance = Instantiate(attackDamageApplierList[i].gameObject, spawnPos, transform.rotation);

                if (instance.TryGetComponent<IDamageApplier>(out var applier)) {
                    applier.Attack = this;
                }
            }
        }
    }
}