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

        [SerializeField] private List<IDamageApplier> _attackDamageApplierList = new List<IDamageApplier>();

        public override bool TryAttack() {
            return false;
        }
    }
}