using Assets.Scripts.Gunplay.Guns;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// Represents an inventory for bullets of different cartridge types.
    /// </summary>
    public interface IBulletInventory
    {
        /// <summary>
        /// Gets the current number of bullets of the specified cartridge type.
        /// </summary>
        /// <param name="cartridgeType">
        /// The cartrifge type
        /// </param>
        /// <returns>
        /// The number of bullets of that type
        /// </returns>
        int GetNumberOfBulletsForType(ICartridgeSpec cartridgeType);

        /// <summary>
        /// Adds the specified number of bullets of the specified cartridge type to the inventory.
        /// </summary>
        /// <param name="cartridgeType">
        /// The cartrifge type
        /// </param>
        /// <param name="numberOfBullets">
        /// The number of bullets to add
        /// </param>
        void AddBulletsOfType(ICartridgeSpec cartridgeType, int numberOfBullets);

        /// <summary>
        /// Removes the specified number of bullets of the specified cartridge type from the inventory.
        /// </summary>
        /// <param name="cartridgeType">
        /// The cartrifge type
        /// </param>
        /// <param name="numberOfBullets">
        /// The number of bullets to remove
        /// </param>
        void RemoveBulletsOfType(ICartridgeSpec cartridgeType, int numberOfBullets);
    }
}