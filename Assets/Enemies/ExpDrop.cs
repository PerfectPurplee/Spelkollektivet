using System;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Collider))]
    public class ExpDrop : MonoBehaviour {
        [SerializeField] private int exp;

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent<Player.Player>(out Player.Player player)) return;
            XPManager.Instance.GainExp(exp);
        }
    }
}