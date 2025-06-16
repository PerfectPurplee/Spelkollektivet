namespace Enemies {
    public class EnemyBasicMeleeAttack : EnemyAttack {
        public override float TriggerDistance => 4f;
        public override EnemyAttackType AttackType => EnemyAttackType.Melee;
        public override float AttackDuration => 5f;


        public override bool TryAttack() {
            return EnemyAI.distance < this.TriggerDistance && EnemyAI.State != EnemyAI.EnemyState.Attacking;
        }
    }
}