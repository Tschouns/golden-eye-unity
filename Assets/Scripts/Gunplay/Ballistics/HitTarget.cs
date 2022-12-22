using Assets.Scripts.Gunplay.Effects;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Implements <see cref="IHitTarget"/>. Activates a set of associated hit effects.
    /// </summary>
    public class HitTarget : MonoBehaviour, IHitTarget
    {
        [SerializeField]
        private BallisticMaterial ballisticMaterial;

#if UNITY_EDITOR
        [SerializeField]
        private GameObject debugHitIndicatorPrefab;
#endif

        private IHitEffect[] hitEffects;

        public BallisticMaterial Material => this.ballisticMaterial;

        public void Hit(BulletImpact impact)
        {
#if UNITY_EDITOR
            if (this.debugHitIndicatorPrefab != null)
            {
                Instantiate(
                    this.debugHitIndicatorPrefab,
                    impact.EntryPoint,
                    Quaternion.identity,
                    this.transform
                );

                if (impact.Type == BulletImpactType.Pierced)
                {
                    Instantiate(
                        this.debugHitIndicatorPrefab,
                        impact.ExitPoint,
                        Quaternion.identity,
                        this.transform
                    );
                }
            }
#endif

            Array.ForEach(this.hitEffects, e => e.ReactToImpact(impact));
        }

        private void Awake()
        {
            Debug.Assert(this.ballisticMaterial != null);
            this.ballisticMaterial.Verify();

            // TODO: is this "by-convention-style" smart??
            this.hitEffects = this.GetComponents<IHitEffect>().ToArray();
        }
    }
}
