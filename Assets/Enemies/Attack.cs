using System;
using UnityEngine;

namespace Enemies {
    public abstract class Attack : MonoBehaviour {
        public abstract float TriggerDistance { get; }
        public abstract AttackType AttackType { get; }

        public abstract float AttackDuration { get; }
        public abstract int Damage { get; }
        protected Animator AnimatorController { get; set; }

        private void Awake() {
            AnimatorController = GetComponent<Animator>();
        }

        public abstract bool TryAttack();
    }
}