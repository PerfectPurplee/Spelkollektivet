using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies {
    public class EnemyBasicMeleeAttack : EnemyAttack {
        public override float TriggerDistance => 4f;
        public override EnemyAttackType AttackType => EnemyAttackType.Melee;
        public override float AttackDuration => 5f;
        public override int Damage => 10;

        [FormerlySerializedAs("meleeWeaponHitbox")] [SerializeField]
        private MeleeWeaponHitbox meleeHeaponWitbox;


        public override bool TryAttack() {
            if (EnemyAI.distance < this.TriggerDistance && EnemyAI.State != EnemyAI.EnemyState.Attacking) {
                meleeHeaponWitbox.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                return true;
            }

            return false;
        }
    }
}