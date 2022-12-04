using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Misc;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Implements the mechanics of a gun.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Gun : MonoBehaviour, IGun, ITogglePhysics
    {
        [SerializeField]
        private Transform muzzle;

        [SerializeField]
        private GunProperties properties;

        [SerializeField]
        private Rigidbody myRigidbody;

        [SerializeField]
        private bool startWithPhysics = false;

        private bool isTriggerActive = false;
        private bool isTriggerEntryProcessed = false;
        private bool isReady = true;

        public string UniqueName => this.properties.UniqueName;

        public void Trigger()
        {
            this.isTriggerActive = true;
        }

        public void ActivatePhysics()
        {
            this.myRigidbody.isKinematic = false;
            this.myRigidbody.useGravity = true;
            this.myRigidbody.constraints = RigidbodyConstraints.None;
        }

        public void DeactivatePhysics()
        {
            this.myRigidbody.isKinematic = true;
            this.myRigidbody.useGravity = false;
            this.myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void Awake()
        {
            Debug.Assert(this.muzzle != null);
            Debug.Assert(this.properties != null);
            Debug.Assert(this.myRigidbody != null);
            this.properties.Verify();

            if (this.startWithPhysics)
            {
                this.ActivatePhysics();
            }
            else
            {
                this.DeactivatePhysics();
            }
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

            var randomizedShotDirection = BallisticsHelper.RandomRotate(this.muzzle.forward, this.properties.MaxDeviationRadians);

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