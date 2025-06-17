using Interface;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Collider))]
    public class RangeAttackHitbox : MonoBehaviour, IDamageApplier {
        public Attack Attack { get; set; }


        private void OnTriggerEnter(Collider other) {
            ((IDamageApplier)this).TryDealDamage(other.gameObject, Attack);
        }
    }
}