using Assets.Scripts.Gunplay.Guns;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Represents the ability of a character to handle (carry, shoot, etc.) a gun.
    /// </summary>
    public interface IGunHandler
    {
        /// <summary>
        /// Gets character's the gun.
        /// </summary>
        IGun Gun { get; }

        /// <summary>
        /// Gets a value indicating whether the gun is currently equipped.
        /// </summary>
        bool IsEquipped { get; }

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
    }
}