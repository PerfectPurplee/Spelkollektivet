using System.Collections.Generic;
using Enemies;
using Interface;
using UnityEngine;

namespace Boss {
    public class BossMeleeAttack : BossAttack {
        public override float TriggerDistance => 4f;
        public override AttackType AttackType => AttackType.Melee;
        public override float AttackDuration => 3f;
        public override int Damage => 20;

        public override string AnimatorAttackName {
            get => "MeleeAttack";
            set { }
        }

        [SerializeField] private List<IDamageApplier> _attackDamageApplierList;

        public override bool TryAttack() {
            if (GameBoss.distanceToPlayer < this.TriggerDistance && GameBoss.BossState != BossState.Attacking) {
                // przekazac info do animatora. Odpalic korutyne czekajaca na koniec animacji. Zeby powrocila bossowi stan walking. 
                // zinstantietowac damageAppliery
                InvokeOnBossAttack();
                return true;
            }

            return false;
        }
    }
}