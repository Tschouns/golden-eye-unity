using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Specifies the property of a "ballistic material", i.e. a material which can be impacted by bullets.
    /// </summary>
    [CreateAssetMenu(fileName = "BallisticMaterial", menuName = "Scriptable Objects/Ballistic Material")]
    public class BallisticMaterial : ScriptableObject, IVerifyable
    {
        [SerializeField]
        private float penetrateAtVelocity = 100f;

        [SerializeField]
        private float pierceAtVelocity = 200f;

        [SerializeField]
        private float bouncyness = 0.5f;

        /// <summary>
        /// Gets the velocity required to penetrate the material.
        /// </summary>
        public float PenetrateAtVelocity => this.penetrateAtVelocity;

        /// <summary>
        /// Gets the velocity required to pierce through an object of this material.
        /// </summary>
        public float PierceAtVelocity => this.pierceAtVelocity;

        /// <summary>
        /// Gets the "bouncyness" factor of this material, i.e. how much energy is preserved/absorbed in case of a ricochet.
        /// A value between 0 and 1.
        /// </summary>
        public float Bouncyness => this.bouncyness;

        public void Verify()
        {
            Debug.Assert(this.penetrateAtVelocity > 0);
            Debug.Assert(this.pierceAtVelocity > this.penetrateAtVelocity);
            Debug.Assert(this.bouncyness >= 0 && this.bouncyness <= 1);
        }
    }
}
