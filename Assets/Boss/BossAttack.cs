using Enemies;

namespace Boss {
    public abstract class BossAttack : Attack {
        protected GameBoss GameBoss { get; private set; }

        private void Awake() {
            GameBoss = GetComponent<GameBoss>();
        }
    }
}