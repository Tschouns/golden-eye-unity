using Assets.Scripts.Gunplay.Ballistics;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Implements the mechanics of a gun.
    /// </summary>
    public class Gun : MonoBehaviour, IShoot
    {
        [SerializeField]
        private Transform muzzle;

        // TODO: make configurable by scriptable object
        [SerializeField]
        private GunProperties properties;

        private bool isTriggerActive = false;
        private bool isTriggerEntryProcessed = false;
        private bool isReady = true;

        public void Trigger()
        {
            this.isTriggerActive = true;
        }

        private void Awake()
        {
            Debug.Assert(this.muzzle != null);
            Debug.Assert(this.properties != null);
            this.properties.Verify();
        }

        private void Update()
        {
            // Determine whether the trigger has been pulled in this very frame (as opposed to being held).
            var isTriggerPulledNow = this.isTriggerActive && !this.isTriggerEntryProcessed;

            // Shoot.
            if (isTriggerPulledNow ||
                (this.properties.IsFullyAutomatic && this.isTriggerActive))
            {
                this.TryShoot();
            }

            // Track / reset.
            this.isTriggerEntryProcessed = this.isTriggerActive;
            this.isTriggerActive = false;
        }

        private bool TryShoot()
        {
            if (!this.isReady)
            {
                return false;
            }

            this.properties.ShootSound.Play(this.muzzle.position);

            // TODO: add to gun properties.
            var randomizedShotDirection = BallisticsHelper.RandomRotate(this.muzzle.up, 0.08f);

            // TODO: add layer mask?
            BallisticsHelper.ShootProjectile(
                this.muzzle.position,
                randomizedShotDirection,
                this.properties.Cartridge.BulletMass,
                this.properties.Cartridge.MuzzleVelocity);

            // Cooldown.
            this.StartCoroutine(this.ResetReadynessAfter(1f / this.properties.FireRate));

            return true;
        }

        private IEnumerator ResetReadynessAfter(float seconds)
        {
            this.isReady = false;
            yield return new WaitForSeconds(seconds);

            this.isReady = true;
            yield return null;
        }
    }
}