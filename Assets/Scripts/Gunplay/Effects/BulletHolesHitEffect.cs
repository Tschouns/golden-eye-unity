using Assets.Scripts.Gunplay.Ballistics;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Spawns a bullet hole prefab onto the hit target.
    /// </summary>
    public class BulletHolesHitEffect : MonoBehaviour, IHitEffect
    {
        public void ReactToImpact(BulletImpact impact)
        {
            if (impact.HitMaterial.BulletEntryHolePrefabs == null ||
                !impact.HitMaterial.BulletEntryHolePrefabs.Any())
            {
                return;
            }

            // Randomly select and spawn bullet hole prefab.
            this.SpawnBulletHolePrefab(impact.HitMaterial.BulletEntryHolePrefabs.ToList(), impact.EntryPoint, impact.EntrySurfaceNormal);

            if (impact.Type == BulletImpactType.Pierced)
            {
                this.SpawnBulletHolePrefab(impact.HitMaterial.BulletExitHolePrefabs.ToList(), impact.ExitPoint, impact.ExitSurfaceNormal);
            }
        }

        private void SpawnBulletHolePrefab(IReadOnlyList<GameObject> bulletHolePrefabs, Vector3 position, Vector3 pointingDirection)
        {
            // Randomly select bullet hole prefab.
            var i = Random.Range(0, bulletHolePrefabs.Count());
            var prefab = bulletHolePrefabs.ToList()[i];

            // Spawn.
            var instance = Instantiate(prefab, position, Quaternion.identity, this.transform);

            // Orient properly, random rotate around its pointing direction axis.
            instance.transform.LookAt(instance.transform.position + pointingDirection);
            instance.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
        }
    }
}
