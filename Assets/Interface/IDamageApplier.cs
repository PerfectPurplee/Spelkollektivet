using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Interface {
    public interface IDamageApplier {
        Attack Attack { get; set;}

        public bool TryDealDamage(GameObject obj, Attack attack) {
            if (obj.TryGetComponent<IDamageable>(out IDamageable damageable)) {
                damageable.TakeDamage(attack.Damage);
                return true;
            }

            return false;
        }
    }
}