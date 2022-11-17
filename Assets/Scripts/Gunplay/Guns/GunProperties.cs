using Assets.Scripts.Misc;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Specifies the properties of a specific type of gun.
    /// </summary>
    [CreateAssetMenu(fileName = "GunProperties", menuName = "Scriptable Objects/Gun Properties")]
    public class GunProperties : ScriptableObject, IVerifyable
    {
        [SerializeField]
        private CartridgeSpec cartridge;

        [SerializeField]
        [Range(1, 100)]
        private int fireRate = 3;

        [SerializeField]
        private bool isFullyAutomatic = false;

        [SerializeField]
        [Range(0, 30)]
        private float maxDeviationDegrees = 5f;

        [SerializeField]
        private SimpleSoundEmitter shootSound;

        /// <summary>
        /// Gets the cartridge specification.
        /// </summary>
        public CartridgeSpec Cartridge => this.cartridge;

        /// <summary>
        /// Gets the gun's fire rate.
        /// </summary>
        public int FireRate => this.fireRate;

        /// <summary>
        /// Gets a value indicating whether the gun is fully automatic, i.e. the trigger can be held down to rapid-fire.
        /// </summary>
        public bool IsFullyAutomatic => this.isFullyAutomatic;

        /// <summary>
        /// Gets the maximum angular deviation [rad] for any shot fired.
        /// </summary>
        public float MaxDeviationRadians => this.maxDeviationDegrees * Mathf.Deg2Rad;

        /// <summary>
        /// Gets the sound to play when shooting.
        /// </summary>
        public SimpleSoundEmitter ShootSound => this.shootSound;

        public void Verify()
        {
            Debug.Assert(this.cartridge != null);
            Debug.Assert(this.fireRate > 0);
            Debug.Assert(this.shootSound != null);
            this.shootSound.Verify();
        }
    }
}
