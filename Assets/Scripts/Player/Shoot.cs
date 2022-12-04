using Assets.Scripts.Characters;
using Assets.Scripts.Controls;
using Assets.Scripts.Gunplay.Guns;
using Assets.Scripts.Gunplay.Inventory;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Allows a character to have and manipulate a gun.
    /// </summary>
    public class Shoot : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private GunInventory inventory;

        [SerializeField]
        private Transform gunHand;

        private Gun activeGun;

        public void NotifyOnDied()
        {
            this.enabled = false;

            // Drop the gun.
            this.activeGun?.ActivatePhysics();
        }

        private void Awake()
        {
            Debug.Assert(this.inventory != null);
            Debug.Assert(this.gunHand != null);
        }

        private void Update()
        {
            if (this.activeGun != null &&
                ControlsProvider.Actions.Trigger)
            {
                this.activeGun.Trigger();
            }

            if (ControlsProvider.Actions.CycleWeapon)
            {
                this.CycleGuns();
            }
        }

        private void CycleGuns()
        {
            if (this.activeGun != null)
            {
                GunHelper.HideGun(this.activeGun);
            }

            // Switch gun.
            this.activeGun = this.inventory.GetNextGun();

            if (this.activeGun != null)
            {
                GunHelper.ProduceGun(this.activeGun, this.gunHand);
            }
        }
    }
}
