using Assets.Scripts.Characters;
using Assets.Scripts.Gunplay.Guns;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// Allows a character to pick up stuff.
    /// </summary>
    public class PickUp : MonoBehaviour
    {
        [SerializeField]
        private GunInventory gunInventory;

        [SerializeField]
        private BulletInventory bulletInventory;

        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private SimpleSoundEmitter pickupGunSound;

        private void Awake()
        {
            Debug.Assert(this.gunInventory != null, "Gun inventory is not set.");
            Debug.Assert(this.bulletInventory != null, "Bullet inventory is not set.");
            Debug.Assert(this.gunHandler != null, "Gun handler is not set.");
            Debug.Assert(this.pickupGunSound != null, "Pickup gun sound is not set.");
            this.pickupGunSound.Verify();
        }

        private void OnTriggerEnter(Collider other)
        {
            var gun = other.GetComponentInParent<Gun>();
            if (gun == null)
            {
                return;
            }

            // Prevents the gun in-hand from being "picked up".
            if (object.ReferenceEquals(gun, this.gunHandler.ActiveGun))
            {
                return;
            }

            if (this.gunInventory.Contains(gun))
            {
                var numberOfBullets = this.bulletInventory.GetNumberOfBulletsForType(gun.Properties.Cartridge);
                if (numberOfBullets < gun.Properties.Cartridge.MaxNumberOfInventoryBullets)
                {
                    // Pick up just the bullets.
                    this.bulletInventory.AddBulletsOfType(gun.Properties.Cartridge, gun.CurrentNumberOfBullets);
                    GunHelper.HideGun(gun);
                    this.pickupGunSound.Play(this.transform.position);
                }
            }
            else
            {
                // Pick up the new gun.
                this.gunInventory.AddGun(gun);
                GunHelper.HideGun(gun);
                this.pickupGunSound.Play(this.transform.position);
            }
        }
    }
}
