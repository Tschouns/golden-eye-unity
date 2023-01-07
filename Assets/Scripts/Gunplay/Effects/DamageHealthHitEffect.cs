using Assets.Scripts.Damage;
using Assets.Scripts.Gunplay.Ballistics;
using System;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Causes damage to a target's health when hit.
    /// </summary>
    [RequireComponent(typeof(HitTarget))]
    public class DamageHealthHitEffect : MonoBehaviour, IHitEffect
    {
        [SerializeField]
        private float minEnergyDamageConversionFactor = 0.1f;

        [SerializeField]
        private float maxEnergyDamageConversionFactor = 0.3f;

        [SerializeField]
        private Health health;

        public void ReactToImpact(BulletImpact impact)
        {
            // Convert the impact energy into healt damage.
            var conversionFactor = UnityEngine.Random.Range(
                this.minEnergyDamageConversionFactor,
                this.maxEnergyDamageConversionFactor);

            var damage = Convert.ToInt32(impact.GetKineticEnergy() * conversionFactor);

            this.health.Damage(damage);
        }

        private void Awake()
        {
            Debug.Assert(this.health != null, "Health is not set.");
        }
    }
}
