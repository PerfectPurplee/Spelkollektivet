using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Boss {
    public class BossBulletHellAttack : BossAttack {
        public override float TriggerDistance => 20f;
        public override AttackType AttackType => AttackType.Ranged;
        public override float AttackDuration => 5f;
        public override int Damage => 5;

        public override string AnimatorAttackName {
            get => "BulletAttack";
            set { }
        }

        [SerializeField] protected List<BossDamageApplier> attackDamageApplierList;
        [SerializeField] private int projectileCount = 36; // Number of projectiles around the boss
        [SerializeField] private float radius = 2f; // Distance from the boss

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
            if (attackDamageApplierList == null || attackDamageApplierList.Count == 0)
                return;

            GameObject bulletPrefab = attackDamageApplierList[0].gameObject;

            for (int i = 0; i < projectileCount; i++) {
                float angle = i * (360f / projectileCount);
                float rad = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * radius;
                Vector3 spawnPos = transform.position + offset;

                // Look direction: away from the boss (like an explosion)
                Quaternion rotation = Quaternion.LookRotation(offset.normalized);

                GameObject instance = Instantiate(bulletPrefab, spawnPos, rotation);

                if (instance.TryGetComponent<IDamageApplier>(out var applier)) {
                    applier.Attack = this;
                }
            }
        }
    }
}