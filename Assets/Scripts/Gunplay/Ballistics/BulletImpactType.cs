namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Specifies the different possible types of bullet impacts.
    /// </summary>
    public enum BulletImpactType
    {
        /// <summary>
        /// The bullet was deflected from the surface of the hit target.
        /// </summary>
        Deflected = 1,

        /// <summary>
        /// The bullet has penetrated the hit target, and has come to a halt inside the target.
        /// </summary>
        Penetrated = 2,

        /// <summary>
        /// The bullet has fully pierced the target, and has exited the target at the exit point.
        /// </summary>
        Pierced = 3,
    }
}
