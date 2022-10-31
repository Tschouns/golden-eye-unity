using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Specifies the properties of a specific cartridge.
    /// </summary>
    [CreateAssetMenu(fileName = "CartridgeSpec", menuName = "Scriptable Objects/Cartridge Spec")]
    public class CartridgeSpec : ScriptableObject, IVerifyable
    {
        [SerializeField]
        [Range(0.0001f, 1f)]
        private float bulletMass = 0.00745f; // 9x19mm Para. FMJ

        [SerializeField]
        [Range(100f, 5000f)]
        private float muzzleVelocity = 360; // 9x19mm Para. FMJ

        /// <summary>
        /// Gets the mass of the bullets.
        /// </summary>
        public float BulletMass => this.bulletMass;

        /// <summary>
        /// Gets the muzzle velocity.
        /// </summary>
        public float MuzzleVelocity => this.muzzleVelocity;

        public void Verify()
        {
            Debug.Assert(this.bulletMass > 0);
            Debug.Assert(this.muzzleVelocity > 0);
        }
    }
}
