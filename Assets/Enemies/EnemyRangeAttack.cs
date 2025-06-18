using UnityEngine;

namespace Enemies {
    public class EnemyRangeAttack : EnemyAttack {
        public override float TriggerDistance => 10f;
        public override AttackType AttackType => AttackType.Ranged;
        public override float AttackDuration => 4f;
        public override int Damage => 5;


        [SerializeField] private float coneAngle = 0.8f;

        [SerializeField] private GameObject arrowObject;

        public override bool TryAttack() {
            if (EnemyAI.distance > this.TriggerDistance || EnemyAI.State == EnemyAI.EnemyState.Attacking)
                return false;

            Vector3 directionToPlayer = (Player.Player.Instance.transform.position - transform.position).normalized;

            float dot = Vector3.Dot(transform.forward, directionToPlayer);
            if (dot < coneAngle)
                return false;
            if (Physics.Raycast(transform.position + Vector3.up * 1f, directionToPlayer, out RaycastHit hit,
                    TriggerDistance, 0)) {
                Debug.Log("Hit Obstacle, wont attack");
                return false;
            }

            PerformAttack(directionToPlayer);
            return true;
        }

        public void PerformAttack(Vector3 directionToPlayer) {
            Vector3 spawnPosition =
                transform.position + Vector3.up * 1.5f + transform.forward * 1f;
            Quaternion
                rotation = Quaternion.LookRotation(directionToPlayer);

            var arrow = GameObject.Instantiate(arrowObject, spawnPosition, rotation);
            RangeAttackHitbox hitbox = arrow.GetComponent<RangeAttackHitbox>();
            hitbox.Attack = this;
        }
    }
}