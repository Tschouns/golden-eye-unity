using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Specifies the properties of a specific cartridge.
    /// </summary>
    [CreateAssetMenu(fileName = "CartridgeSpec", menuName = "Scriptable Objects/Cartridge Spec")]
    public class CartridgeSpec : ScriptableObject, ICartridgeSpec, IVerifyable
    {
        [SerializeField]
        private string uniqueName;

        [SerializeField]
        private float bulletMass = 0.00745f; // 9x19mm Para. FMJ

        [SerializeField]
        private float muzzleVelocity = 360; // 9x19mm Para. FMJ

        [SerializeField]
        private float bulletDragFactor = 1f;

        [SerializeField]
        private int maxNumberOfBullets = 100;

        [SerializeField]
        private int startingNumberOfBullets = 30;

        public string UniqueName => this.uniqueName;
        public float BulletMass => this.bulletMass;
        public float MuzzleVelocity => this.muzzleVelocity;
        public int MaxNumberOfInventoryBullets => this.maxNumberOfBullets;
        public int InitialNumberOfInventoryBullets => this.startingNumberOfBullets;
        public float BulletDragFactor => this.bulletDragFactor;

        public void Verify()
        {
            Debug.Assert(!string.IsNullOrEmpty(this.uniqueName));
            Debug.Assert(this.bulletMass > 0);
            Debug.Assert(this.muzzleVelocity > 0);
            Debug.Assert(this.bulletDragFactor > 0);
            Debug.Assert(this.maxNumberOfBullets > 0);
            Debug.Assert(this.startingNumberOfBullets > 0);
        }
    }
}
