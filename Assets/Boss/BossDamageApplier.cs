using System;
using Enemies;
using Interface;
using UnityEngine;

namespace Boss {
    [RequireComponent(typeof(Collider))]
    public class BossDamageApplier : MonoBehaviour, IDamageApplier {
        public Attack Attack { get; set; }

        public static event EventHandler OnDamageApplied;


        // prevents player getting struck two times by the same ability
        private bool _playerGotHit;


        private void Awake() {
            _playerGotHit = false;
        }

        void Start() {
        }

        void Update() {
        }

        private void OnTriggerEnter(Collider other) {
            if (_playerGotHit) return;

            if (((IDamageApplier)this).TryDealDamage(other.gameObject, Attack, transform.position)) {
                _playerGotHit = true;
                OnDamageApplied?.Invoke(null, EventArgs.Empty);
            }
        }


        public void OnAnimationAttackFinished() {
            Destroy(gameObject);
        }
    }
}