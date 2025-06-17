using UnityEngine;

namespace Interface {
    public interface IDamageApplier {
        public bool TryDealDamage(GameObject obj, int damage) {
            if (obj.TryGetComponent<IDamageable>(out IDamageable damageable)) {
                damageable.TakeDamage(damage);
            }

            return false;
        }
    }
}