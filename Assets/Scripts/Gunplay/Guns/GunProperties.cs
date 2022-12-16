using Assets.Scripts.Misc;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Specifies the properties of a specific type of gun.
    /// </summary>
    [CreateAssetMenu(fileName = "GunProperties", menuName = "Scriptable Objects/Gun Properties")]
    public class GunProperties : ScriptableObject, IGunProperties, IVerifyable
    {
        [SerializeField]
        private string uniqueName = string.Empty;

        [SerializeField]
        private CartridgeSpec cartridge;

        [SerializeField]
        [Range(1, 100)]
        private int fireRate = 3;

        [SerializeField]
        private bool isFullyAutomatic = false;

        [SerializeField]
        private bool isDoubleAction = false;

        [SerializeField]
        private int clipSize = 10;

        [SerializeField]
        private float reloadTime = 1.5f;

        [SerializeField]
        [Range(0, 30)]
        private float maxDeviationDegrees = 5f;

        [SerializeField]
        private SimpleSoundEmitter shootSound;

        [SerializeField]
        private SimpleSoundEmitter dryFireSound;

        [SerializeField]
        private SimpleSoundEmitter reloadSound;

        public string UniqueName => this.uniqueName;
        public ICartridgeSpec Cartridge => this.cartridge;
        public int FireRate => this.fireRate;
        public bool IsFullyAutomatic => this.isFullyAutomatic;
        public bool IsDoubleAction => this.isDoubleAction;
        public int ClipSize => this.clipSize;
        public float ReloadTime => this.reloadTime;
        public float MaxDeviationRadians => this.maxDeviationDegrees * Mathf.Deg2Rad;

        /// <summary>
        /// Gets the sound to play when shooting.
        /// </summary>
        public SimpleSoundEmitter ShootSound => this.shootSound;

        /// <summary>
        /// Gets the sound to play when dry-firing.
        /// </summary>
        public SimpleSoundEmitter DryFireSound => this.dryFireSound;

        /// <summary>
        /// Gets the sound to play when reloading.
        /// </summary>
        public SimpleSoundEmitter ReloadSound => this.reloadSound;

        public void Verify()
        {
            Debug.Assert(!string.IsNullOrEmpty(this.uniqueName));
            Debug.Assert(this.cartridge != null);
            Debug.Assert(this.fireRate > 0);
            Debug.Assert(this.shootSound != null);
            Debug.Assert(this.dryFireSound != null);
            Debug.Assert(this.reloadSound != null);

            this.shootSound.Verify();
            this.dryFireSound.Verify();
            this.reloadSound.Verify();
        }
    }
}
