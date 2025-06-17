using System;
using Interface;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies {
    [RequireComponent(typeof(Collider))]
    public class MeleeAttackHitbox : MonoBehaviour, IDamageApplier {
        public Attack Attack { get; set; }
        public event EventHandler OnAttackHitSuccess;
        public Transform attacker;

        private void OnTriggerEnter(Collider other) {
            if (((IDamageApplier)this).TryDealDamage(other.gameObject, Attack, attacker.position)) {
                OnAttackHitSuccess?.Invoke(this, EventArgs.Empty);
            }
            
        }

        private void OnTriggerExit(Collider other) {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}