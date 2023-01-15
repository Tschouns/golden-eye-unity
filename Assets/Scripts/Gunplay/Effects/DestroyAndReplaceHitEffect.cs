using Assets.Scripts.Gunplay.Ballistics;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Destroys the hit target, and --optionally-- replaces it with a replacement prefab.
    /// </summary>
    public class DestroyAndReplaceHitEffect : MonoBehaviour, IHitEffect
    {
        private bool done = false;

        [SerializeField]
        private GameObject optionalReplacementPrefab;

        [SerializeField]
        private bool includeDeflected = false;

        public void ReactToImpact(BulletImpact impact)
        {
            if (this.done)
            {
                return;
            }

            if (this.includeDeflected ||
                impact.Type is BulletImpactType.Penetrated or BulletImpactType.Pierced)
            {
                // Replace.
                if (this.optionalReplacementPrefab != null)
                {
                    Instantiate(
                        this.optionalReplacementPrefab,
                        this.transform.position,
                        this.transform.rotation,
                        this.transform.parent);
                }

                // Destroy.
                DestroyImmediate(this.gameObject);

                this.done = true;
            }
        }

        private void Awake()
        {
        }
    }
}
