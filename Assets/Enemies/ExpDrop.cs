using System;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Collider))]
    public class ExpDrop : MonoBehaviour {
        [SerializeField] private int exp;


        public void Initialize(int experience) {
            //this.exp = experience;
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent<Player.Player>(out Player.Player player)) return;
            XPManager.Instance.GainExp(exp);
            Destroy(gameObject);
        }
    }
}