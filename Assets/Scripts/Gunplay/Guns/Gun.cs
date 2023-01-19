using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Misc;
using Assets.Scripts.Noise;
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

        [SerializeField]
        private bool startLoaded = true;

        [SerializeField]
        private AbstractShotEffect[] shotEffects = new AbstractShotEffect[0];

        private INoiseEventBus noiseEventBus;
        private bool isTriggerActive = false;
        private bool isTriggerEntryProcessed = false;
        private bool isReady = true;
        private bool isCocked = true;

        public IGunProperties Properties => this.properties;

        public int CurrentNumberOfBullets { get; private set; }

        public void Trigger()
        {
            this.isTriggerActive = true;
        }

        public int Reload(int availableNumberOfBullets)
        {
            if (this.CurrentNumberOfBullets >= this.Properties.ClipSize)
            {
                return 0;
            }

            var numberOfBulletsToTake = Mathf.Min(availableNumberOfBullets, this.Properties.ClipSize - this.CurrentNumberOfBullets);
            this.CurrentNumberOfBullets += numberOfBulletsToTake;

            this.isCocked = true;
            this.properties.ReloadSound.Play(this.transform.position);
            this.StartCoroutine(this.ResetReadynessAfter(1f / this.properties.FireRate));

            return numberOfBulletsToTake;
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
            Debug.Assert(this.shotEffects != null);
            this.properties.Verify();

            this.noiseEventBus = FindObjectOfType<NoiseEventBus>();
            Debug.Assert(this.noiseEventBus != null);

            if (this.startWithPhysics)
            {
                this.ActivatePhysics();
            }
            else
            {
                this.DeactivatePhysics();
            }

            if (this.startLoaded)
            {
                this.CurrentNumberOfBullets = this.properties.ClipSize;
            }
            else
            {
                this.CurrentNumberOfBullets = 0;
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
            if (!this.isReady ||
                !this.isCocked)
            {
                return false;
            }

            if (this.CurrentNumberOfBullets <= 0)
            {
                // Dry-fire.
                this.properties.DryFireSound.Play(this.transform.position);
                this.StartCoroutine(this.ResetReadynessAfter(1f / this.properties.FireRate));

                if (!this.properties.IsDoubleAction)
                {
                    this.isCocked = false;
                }

                return false;
            }

            // Shoot.
            var randomizedShotDirection = TransformHelper.RandomRotate(this.muzzle.forward, this.properties.MaxDeviationRadians);
            BallisticsHelper.ShootProjectile(
                this.muzzle.position,
                randomizedShotDirection,
                this.properties.Cartridge.BulletMass,
                this.properties.Cartridge.MuzzleVelocity,
                this.properties.Cartridge.BulletDragFactor);

            this.CurrentNumberOfBullets--;
            this.properties.ShootSound.Play(this.muzzle.position);
            this.noiseEventBus.ProduceNoise(NoiseType.GunShot, this.muzzle.position, this.properties.AudibleDistance);
            this.PlayShotEffects();

            // Cooldown.
            this.StartCoroutine(this.ResetReadynessAfter(1f / this.properties.FireRate));

            return true;
        }

        private void PlayShotEffects()
        {
            if (this.shotEffects == null)
            {
                return;
            }

            foreach (var effect in this.shotEffects)
            {
                effect?.Play();
            }
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