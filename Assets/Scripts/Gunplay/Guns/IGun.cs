namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Represents a gun which shoots. Exposes methods necessary to manipulate the gun.
    /// </summary>
    public interface IGun
    {
        /// <summary>
        /// Gets the properties of the gun.
        /// </summary>
        IGunProperties Properties { get; }

        /// <summary>
        /// Gets the number of remaining bullets in the gun.
        /// </summary>
        int CurrentNumberOfBullets { get; }

        /// <summary>
        /// Pulls (or holds) the trigger on the gun.
        /// </summary>
        void Trigger();

        /// <summary>
        /// Reloads the gun, using the specified available number of bullets, and returns the number of bullets
        /// actually loaded into the gun.
        /// </summary>
        /// <param name="availableNumberOfBullets">
        /// The number of bullets available
        /// </param>
        /// <returns>
        /// The number of bullets actually loaded into the gun
        /// </returns>
        int Reload(int availableNumberOfBullets);
    }
}
