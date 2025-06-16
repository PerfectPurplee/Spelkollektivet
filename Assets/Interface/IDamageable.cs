namespace Interface {
    public interface IDamageable {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        void TakeDamage(int damage);
    }
}