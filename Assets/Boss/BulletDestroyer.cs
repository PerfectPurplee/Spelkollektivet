using System;
using UnityEngine;

namespace Boss {
    public class BulletDestroyer : MonoBehaviour {
        private Vector3 _direction;
        public float speed = 10f;


        private void Start() {
            Boss.BossDamageApplier.OnDamageApplied += BossDamageApplierOnOnDamageApplied;
            _direction = transform.forward;
        }

        private void OnDestroy() {
            Boss.BossDamageApplier.OnDamageApplied -= BossDamageApplierOnOnDamageApplied;
        }

        private void Update() {
            transform.position += _direction * (speed * Time.deltaTime);
        }

        private void BossDamageApplierOnOnDamageApplied(object sender, EventArgs e) {
            Destroy(gameObject);
        }
    }
}