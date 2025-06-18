using System;
using Interface;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Collider))]
    public class RangeAttackHitbox : MonoBehaviour, IDamageApplier {
        public Attack Attack { get; set; }

        [SerializeField] private float speed = 20f;
        private Vector3 _direction;

        private void Start() {
            _direction = transform.forward;
        }

        private void Update() {
            transform.position += _direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if (((IDamageApplier)this).TryDealDamage(other.gameObject, Attack, transform.position)) {
                Debug.Log("Range hit and damage dealt");

                // do something

                Destroy(gameObject);
            }
        }
    }
}