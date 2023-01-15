using Assets.Scripts.Gunplay.Ballistics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Spawns all prefabs (particleEffects) for the different <see cref="BulletImpactType"/> when hit.
    /// </summary>
    public class ParticleHitEffect : MonoBehaviour, IHitEffect
    {
        public void ReactToImpact(BulletImpact impact)
        {
            this.SpawnEffects(
                impact.HitMaterial.BaseParticleEffectPrefabs,
                impact.EntryPoint,
                impact.EntryPoint - impact.EntryDirectionNormalized);

            switch (impact.Type)
            {
                case BulletImpactType.Deflected:
                    this.SpawnEffects(
                        impact.HitMaterial.DeflectedParticleEffectPrefabs,
                        impact.ExitPoint,
                        impact.ExitPoint + impact.ExitDirectionNormalized);
                    break;
                case BulletImpactType.Pierced:
                    this.SpawnEffects(
                        impact.HitMaterial.PiercedParticleEffectPrefabs,
                        impact.ExitPoint,
                        impact.ExitPoint + impact.ExitDirectionNormalized);
                    break;
            }
        }

        private void SpawnEffects(IEnumerable<GameObject> prefabsNullSafe, Vector3 position, Vector3 lookAtPoint)
        {
            if (prefabsNullSafe == null)
            {
                return;
            }

            foreach (var prefab in prefabsNullSafe)
            {
                if (prefab != null)
                {
                    var effect = Instantiate(
                        prefab,
                        position,
                        Quaternion.identity);

                    effect.transform.LookAt(lookAtPoint, Vector3.up);
                }
            }
        }
    }
}
