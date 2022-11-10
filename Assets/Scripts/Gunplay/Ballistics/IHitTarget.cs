namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Represents a target which can be hit by bullets.
    /// </summary>
    public interface IHitTarget
    {
        /// <summary>
        /// Gets the ballistic material.
        /// </summary>
        BallisticMaterial Material { get; }

        /// <summary>
        /// Hits the target with a bullet.
        /// </summary>
        /// <param name="impact">
        /// The bullet impact information
        /// </param>
        void Hit(BulletImpact impact);
    }
}
