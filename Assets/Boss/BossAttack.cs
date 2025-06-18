using System;
using Enemies;

namespace Boss {
    public abstract class BossAttack : Attack {
        public class BossAttackArgs : EventArgs {
            public string AttackName { get; }

            public BossAttackArgs(string animatorAttackName) {
                this.AttackName = animatorAttackName;
            }
        }

        public event EventHandler<BossAttackArgs> OnBossAttack;
        public abstract string AnimatorAttackName { get; set; }
        protected GameBoss GameBoss { get; private set; }

        private void Awake() {
            GameBoss = GetComponent<GameBoss>();
        }

        protected void InvokeOnBossAttack() {
            OnBossAttack?.Invoke(this, new BossAttackArgs(AnimatorAttackName));
        }
    }
}