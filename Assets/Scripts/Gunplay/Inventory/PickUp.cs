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
        private SimpleSoundEmitter pickupGunSound;

        private void Awake()
        {
            Debug.Assert(this.gunInventory != null);
            Debug.Assert(this.pickupGunSound != null);
            this.pickupGunSound.Verify();
        }

        private void OnTriggerEnter(Collider other)
        {
            var gun = other.GetComponentInParent<Gun>();
            if (gun == null)
            {
                return;
            }

            // Prevents picking up guns unnecessarily, and prevents the gun in-hand from disappearing.
            if (this.gunInventory.Contains(gun))
            {
                return;
            }

            GunHelper.HideGun(gun);
            this.gunInventory.AddGun(gun);

            // Play sound.
            this.pickupGunSound.Play(this.transform.position);
        }
    }
}
