using Assets.Scripts.Gunplay.Guns;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// See <see cref="BulletInventory"/>.
    /// </summary>
    public class BulletInventory : MonoBehaviour, IBulletInventory
    {
        [SerializeField]
        private CartridgeSpec[] initialBulletTypes;

        private readonly IDictionary<string, int> numberOfBulletsPerAmmoType = new Dictionary<string, int>();

        public int GetNumberOfBulletsForType(ICartridgeSpec cartridgeType)
        {
            this.InitializeCartridgeType(cartridgeType);

            return this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName];
        }

        public void AddBulletsOfType(ICartridgeSpec cartridgeType, int numberOfBullets)
        {
            this.InitializeCartridgeType(cartridgeType);
            this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] = this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] + numberOfBullets;
            this.ClampNumberOfBullets(cartridgeType);
        }

        public void RemoveBulletsOfType(ICartridgeSpec cartridgeType, int numberOfBullets)
        {
            this.InitializeCartridgeType(cartridgeType);
            this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] = this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] - numberOfBullets;
            this.ClampNumberOfBullets(cartridgeType);
        }

        private void Awake()
        {
            if (this.initialBulletTypes != null)
            {
                foreach (var bulletType in this.initialBulletTypes)
                {
                    this.AddBulletsOfType(bulletType, bulletType.InitialNumberOfInventoryBullets);
                }
            }
        }

        private void InitializeCartridgeType(ICartridgeSpec cartridgeType)
        {
            if (this.numberOfBulletsPerAmmoType.ContainsKey(cartridgeType.UniqueName))
            {
                return;
            }

            this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] = 0;
        }

        private void ClampNumberOfBullets(ICartridgeSpec cartridgeType)
        {
            var clampedNumberOfBullets = Mathf.Clamp(this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName], 0, cartridgeType.MaxNumberOfInventoryBullets);
            this.numberOfBulletsPerAmmoType[cartridgeType.UniqueName] = clampedNumberOfBullets;
        }
    }
}