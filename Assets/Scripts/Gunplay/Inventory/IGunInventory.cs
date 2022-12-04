using Assets.Scripts.Gunplay.Guns;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// Represents an inventory of guns.
    /// </summary>
    /// <typeparam name="TGun">
    /// The gun type
    /// </typeparam>
    public interface IGunInventory<TGun>
        where TGun : class, IGun
    {
        /// <summary>
        /// Adds a gun to the inventory.
        /// </summary>
        /// <param name="gun">
        /// A gun
        /// </param>
        void AddGun(TGun gun);

        /// <summary>
        /// Checks whether the inventory already contains a gun like the specified gun.
        /// </summary>
        /// <param name="gun">
        /// The gun to check for
        /// </param>
        /// <returns>
        /// A value indicating whether the inventory already contains a gun like the specified gun
        /// </returns>
        bool Contains(TGun gun);

        /// <summary>
        /// Cycles throught the guns in the inventory, and returns the next gun. Can return null if the inventory is empty.
        /// </summary>
        /// <returns>
        /// The next gun; or null
        /// </returns>
        TGun GetNextGun();
    }
}
