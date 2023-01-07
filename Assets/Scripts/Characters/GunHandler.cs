using Assets.Scripts.Damage;
using Assets.Scripts.Gunplay.Guns;
using Assets.Scripts.Gunplay.Inventory;
using Assets.Scripts.Misc;
using Assets.Scripts.Sound;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Implements <see cref="GunHandler"/>.
    /// </summary>
    public class GunHandler : MonoBehaviour, IGunHandler, INotifyOnDied
    {
        [SerializeField]
        private Gun gun;

        [SerializeField]
        private Transform gunHand;

        [SerializeField]
        private RotateToTarget rotatingArm;

        [SerializeField]
        private Transform gunHolster;

        [SerializeField]
        private bool startEquipped = false;

        [SerializeField]
        private float equipTime = 1f;

        [SerializeField]
        private SimpleSoundEmitter equipSound;

        private bool isMovingWeapon = false;
        private bool isDropped = false;
        private bool isReloading = false;

        public IGun Gun => this.gun;
        public Gun ActiveGun => this.gun;

        public bool IsEquipped { get; private set; }

        public float EquipTime => this.equipTime;

        public void SetActiveGun(Gun activeGun)
        {
            if (this.gun != null)
            {
                GunHelper.HideGun(this.gun);
            }

            this.gun = activeGun;
            
            if (activeGun != null)
            {
                var parent = this.IsEquipped ? this.gunHand : this.gunHolster;
                GunHelper.ProduceGun(activeGun, parent);

                this.isDropped = false;
            }
        }

        public void Equip()
        {
            if (this.gun == null ||
                this.isMovingWeapon ||
                this.isDropped)
            {
                return;
            }

            this.StartCoroutine(this.EquipAnimated());
        }

        public void Unequip()
        {
            if (this.isMovingWeapon ||
                this.isDropped)
            {
                return;
            }

            this.StartCoroutine(this.UnequipAnimated());
        }

        public void Drop()
        {
            if (this.gun == null ||
                this.isDropped)
            {
                return;
            }

            this.gun.transform.parent = null;
            this.IsEquipped = false;
            this.isDropped = true;

            this.gun.ActivatePhysics();
        }

        public void Shoot()
        {
            if (this.gun == null ||
                this.isMovingWeapon ||
                this.isDropped ||
                this.isReloading)
            {
                return;
            }

            if (this.IsEquipped)
            {
                this.Gun.Trigger();
            }
        }

        public int Reload(int availableNumberOfBullets)
        {
            if (this.Gun == null ||
                this.isDropped ||
                this.isReloading)
            {
                return 0;
            }

            // Full?
            if (this.Gun.CurrentNumberOfBullets == this.Gun.Properties.ClipSize)
            {
                return 0;
            }

            // No more bullets?
            if (availableNumberOfBullets <= 0)
            {
                return 0;
            }

            var usedUpBullets = Mathf.Min(
                this.Gun.Properties.ClipSize - this.Gun.CurrentNumberOfBullets,
                availableNumberOfBullets);

            this.StartCoroutine(this.ReloadAnimated(availableNumberOfBullets));

            return usedUpBullets;
        }

        public void NotifyOnDied()
        {
            this.Drop();
        }

        private void Awake()
        {
            Debug.Assert(this.gunHand != null, "Gun hand is not set.");
            Debug.Assert(this.rotatingArm != null, "Rotating arm is not set.");
            Debug.Assert(this.gunHolster != null, "Gun holster is not set.");
            Debug.Assert(this.equipSound != null, "Equip sound is not set.");

            this.equipSound.Verify();

            if (this.startEquipped &&
                this.gun != null)
            {
                this.Equip();
            }
            else
            {
                this.Unequip();
            }
        }

        private IEnumerator EquipAnimated()
        {
            // Take from holster.
            this.gun.transform.parent = this.gunHand;
            this.gun.transform.localPosition = Vector3.zero;
            this.gun.transform.localRotation = Quaternion.identity;
            this.IsEquipped = true;

            this.equipSound.Play(this.gunHand.position);

            this.isMovingWeapon = true;
            this.rotatingArm.SetRotationTarget(0);
            yield return new WaitForSeconds(this.equipTime);

            this.isMovingWeapon = false;
            yield return null;
        }

        private IEnumerator UnequipAnimated()
        {
            this.isMovingWeapon = true;
            this.rotatingArm.SetRotationTarget(90);
            yield return new WaitForSeconds(this.equipTime);

            if (this.gun != null &&
                !this.isDropped)
            {
                // Put in holster.
                this.gun.transform.parent = this.gunHolster;
                this.gun.transform.localPosition = Vector3.zero;
                this.gun.transform.localRotation = Quaternion.identity;
            }

            this.IsEquipped = false;
            this.isMovingWeapon = false;
            yield return null;
        }

        private IEnumerator ReloadAnimated(int availableNumberOfBullets)
        {
            this.isReloading = true;

            // Take arm down.
            this.rotatingArm.SetRotationTarget(90);
            yield return new WaitForSeconds(this.Gun.Properties.ReloadTime / 2);

            // Reload.
            this.Gun.Reload(availableNumberOfBullets);

            // Take arm up.
            this.rotatingArm.SetRotationTarget(0);
            yield return new WaitForSeconds(this.Gun.Properties.ReloadTime / 2);

            this.isReloading = false;
            yield return null;
        }
    }
}