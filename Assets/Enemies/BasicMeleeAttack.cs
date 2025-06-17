using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies {
    public class BasicMeleeAttack : Attack {
        public override float TriggerDistance => 4f;
        public override AttackType AttackType => AttackType.Melee;
        public override float AttackDuration => 5f;
        public override int Damage => 10;

        [SerializeField] private MeleeAttackHitbox sharedMeleeWeaponHitbox;


        public override bool TryAttack() {
            if (EnemyAI.distance < this.TriggerDistance && EnemyAI.State != EnemyAI.EnemyState.Attacking) {
                sharedMeleeWeaponHitbox.Attack = this;
                sharedMeleeWeaponHitbox.GetComponent<Collider>().enabled = true;
                return true;
            }

            return false;
        }
    }
}