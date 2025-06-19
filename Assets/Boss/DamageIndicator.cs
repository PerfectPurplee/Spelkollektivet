using System;
using UnityEngine;

namespace Boss {
    public class DamageIndicator : MonoBehaviour {
        [SerializeField] private Collider damageCollider;
        [SerializeField] private MeshRenderer meshRenderer;

        private void Awake() {
            damageCollider = GetComponent<Collider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void TurnColliderOn() {
            damageCollider.enabled = true;
        }

        private void TurnColliderOff() {
            damageCollider.enabled = false;
        }

        public void TurnMeshOff() {
            meshRenderer.enabled = false;
        }
    }
}