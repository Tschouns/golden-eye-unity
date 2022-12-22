using Assets.Scripts.Gunplay.Ballistics;
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
            SpawnEffects(
                impact.HitMaterial.PenetratedParticleEffectPrefab,
                impact.EntryPoint,
                impact.EntryPoint - impact.EntryDirectionNormalized
            );

            switch (impact.Type)
            {
                case BulletImpactType.Deflected:
                    SpawnEffects(
                        impact.HitMaterial.DeflectedParticleEffectPrefab,
                        impact.ExitPoint,
                        impact.ExitPoint + impact.ExitDirectionNormalized
                    );
                    break;
                case BulletImpactType.Pierced:
                    SpawnEffects(
                        impact.HitMaterial.PiercedParticleEffectPrefab,
                        impact.ExitPoint,
                        impact.ExitPoint + impact.ExitDirectionNormalized
                    );
                    break;
            }
        }

        private void SpawnEffects(GameObject[] prefabs, Vector3 position, Vector3 lookAtPoint)
        {
            foreach (var prefab in prefabs)
            {
                var effect = Instantiate(
                    prefab,
                    position,
                    Quaternion.identity,
                    this.gameObject.transform
                );
                effect.transform.LookAt(lookAtPoint, Vector3.up);
            }
        }
    }
}
