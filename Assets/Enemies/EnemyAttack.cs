namespace Enemies {
    public abstract class EnemyAttack : Attack {
        protected EnemyAI EnemyAI { get; private set; }

        private void Awake() {
            EnemyAI = GetComponent<EnemyAI>();
        }
    }
}