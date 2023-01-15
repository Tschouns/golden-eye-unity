
namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Provides the specifics of a cartridge type.
    /// </summary>
    public interface ICartridgeSpec
    {
        /// <summary>
        /// Gets the name which uniquely identifies the cartridge type.
        /// </summary>
        string UniqueName { get; }

        /// <summary>
        /// Gets the mass of the bullets.
        /// </summary>
        float BulletMass { get; }

        /// <summary>
        /// Gets the muzzle velocity.
        /// </summary>
        float MuzzleVelocity { get; }

        /// <summary>
        /// Gets the bullet "drag factor" (i.e. a factor to modify the velocity loss from piercing targets).
        /// </summary>
        float BulletDragFactor { get; }

        /// <summary>
        /// Gets the maximum number of bullets of this type a character can carry in their inventory.
        /// </summary>
        int MaxNumberOfInventoryBullets { get; }

        /// <summary>
        /// Gets the number of bullets a character initially has (if they have any) of this type.
        /// </summary>
        int InitialNumberOfInventoryBullets { get; }
    }
}