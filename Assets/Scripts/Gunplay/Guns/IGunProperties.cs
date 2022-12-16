
namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Provides all the known properties of a gun.
    /// </summary>
    public interface IGunProperties
    {
        /// <summary>
        /// Gets the name which uniquely identifies the type of gun.
        /// </summary>
        public string UniqueName { get; }

        /// <summary>
        /// Gets the cartridge specification.
        /// </summary>
        public ICartridgeSpec Cartridge { get; }

        /// <summary>
        /// Gets the gun's fire rate.
        /// </summary>
        public int FireRate { get; }

        /// <summary>
        /// Gets a value indicating whether the gun is fully automatic, i.e. the trigger can be held down to rapid-fire.
        /// </summary>
        public bool IsFullyAutomatic { get; }

        /// <summary>
        /// Gets a value indicating whether the gun has a double action trigger, i.e. it can be triggered repeatedly without
        /// the gun actually being fired or cocked manually first.
        /// </summary>
        public bool IsDoubleAction { get; }

        /// <summary>
        /// Gets the gun's clip size.
        /// </summary>
        public int ClipSize { get; }

        /// <summary>
        /// Gets the time it takes to reload the gun.
        /// </summary>
        public float ReloadTime { get; }

        /// <summary>
        /// Gets the maximum angular deviation [rad] for any shot fired.
        /// </summary>
        public float MaxDeviationRadians { get; }
    }
}