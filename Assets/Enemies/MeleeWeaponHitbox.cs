using Interface;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies {
    public class MeleeWeaponHitbox : MonoBehaviour {
        [SerializeField] private EnemyAI enemy;

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable)) {
                damageable.TakeDamage(enemy.GetEnemyAttack().Damage);
            }
        }

        private void OnTriggerExit(Collider other) {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}