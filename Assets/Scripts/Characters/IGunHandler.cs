using Assets.Scripts.Gunplay.Guns;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Represents the ability of a character to handle (carry, shoot, etc.) a gun.
    /// </summary>
    public interface IGunHandler
    {
        /// <summary>
        /// Gets the character's gun.
        /// </summary>
        IGun Gun { get; }

        /// <summary>
        /// Gets a value indicating whether the gun is currently equipped.
        /// </summary>
        bool IsEquipped { get; }

        /// <summary>
        /// Gets the amount of time it takes the character to equip/unequip a gun.
        /// </summary>
        float EquipTime { get; }

        /// <summary>
        /// Makes the character equip the gun.
        /// </summary>
        void Equip();

        /// <summary>
        /// Makes the character unequip the gun.
        /// </summary>
        void Unequip();

        /// <summary>
        /// Makes the character drop the gun.
        /// </summary>
        void Drop();

        /// <summary>
        /// Makes the character shoot the gun.
        /// </summary>
        void Shoot();

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