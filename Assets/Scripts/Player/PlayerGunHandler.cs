using Assets.Scripts.Characters;
using Assets.Scripts.Controls;
using Assets.Scripts.Damage;
using Assets.Scripts.Gunplay.Inventory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Allos the player to have and control guns.
    /// </summary>
    public class PlayerGunHandler : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private GunInventory gunInventory;

        [SerializeField]
        private BulletInventory bulletInventory;

        private bool isChangingWeapon = false;

        public void NotifyOnDied()
        {
            this.enabled = false;

            // Drop the gun.
            this.gunHandler.Drop();
        }

        private void Awake()
        {
            Debug.Assert(this.gunHandler != null);
            Debug.Assert(this.gunInventory != null);
            Debug.Assert(this.bulletInventory != null);
        }

        private void Start()
        {
            this.StartCoroutine(this.CycleGunsDelayed());
        }

        private void Update()
        {
            if (this.gunHandler.ActiveGun != null &&
                ControlsProvider.Actions.Trigger)
            {
                this.gunHandler.Shoot();
            }

            if (this.gunHandler.ActiveGun != null &&
                ControlsProvider.Actions.Reload)
            {
                var availableNumberOfBullets = this.bulletInventory.GetNumberOfBulletsForType(this.gunHandler.Gun.Properties.Cartridge);
                var usedUpBullets = this.gunHandler.Reload(availableNumberOfBullets);
                this.bulletInventory.RemoveBulletsOfType(this.gunHandler.Gun.Properties.Cartridge, usedUpBullets);
            }

            if (!this.isChangingWeapon &&
                ControlsProvider.Actions.CycleWeapon)
            {
                this.StartCoroutine(this.CycleGunsDelayed());
            }
        }

        private IEnumerator CycleGunsDelayed()
        {
            this.isChangingWeapon = true;

            this.gunHandler.Unequip();
            yield return new WaitForSeconds(this.gunHandler.EquipTime + 0.1f);

            // Switch gun.
            var newGun = this.gunInventory.GetNextGun();
            this.gunHandler.SetActiveGun(newGun);
            this.gunHandler.Equip();
            yield return new WaitForSeconds(this.gunHandler.EquipTime + 0.1f);

            this.isChangingWeapon = false;
            yield return null;
        }
    }
}