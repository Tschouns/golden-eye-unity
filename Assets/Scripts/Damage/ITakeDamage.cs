namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Represents the ability to take damage.
    /// </summary>
    public interface ITakeDamage
    {
        /// <summary>
        /// Damages this entity.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage.
        /// </param>
        public void Damage(int damage);
    }
}
