using Assets.Scripts.Misc;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Performs ballistics simulations.
    /// </summary>
    public static class BallisticsHelper
    {
        private static readonly int maxDepth = 20;

        /// <summary>
        /// Simulates shooting a projectile. Calls targets (i.e. implementations of <see cref="IHitTarget"/>) when hit, so they
        /// can react to the bullet impact.
        /// </summary>
        /// <param name="origin">
        /// The point of origin from which the bullet is fired
        /// </param>
        /// <param name="direction">
        /// The shot direction
        /// </param>
        /// <param name="bulletMass">
        /// The bullet mass
        /// </param>
        /// <param name="velocity">
        /// The bullet velocity
        /// </param>
        /// <param name="dragFactor">
        /// A factor which scales the bullets drag (proportional to the velocity loss from piercing a target)
        /// </param>
        public static void ShootProjectile(Vector3 origin, Vector3 direction, float bulletMass, float velocity, float dragFactor)
        {
            CastProjectileInternal(origin, direction, bulletMass, velocity, 0, dragFactor);
        }

        private static void CastProjectileInternal(Vector3 origin, Vector3 direction, float bulletMass, float velocity, int currentDepth, float dragFactor)
        {
            // Recursion safety.
            if (currentDepth > maxDepth)
            {
                Debug.LogWarning("Reached max depth.");
                return;
            }

            currentDepth++;

            // Detect and process hit.
            var shotDirectionNormalized = direction.normalized;

            // TODO: add layer mask?
            var allRaycastHits = Physics.RaycastAll(origin, shotDirectionNormalized);

            foreach (var hit in allRaycastHits.OrderBy(r => r.distance))
            {
                var target = hit.collider.GetComponentInParent<IHitTarget>();
                if (target == null)
                {
                    continue;
                }

                if (target.Material == null)
                {
                    Debug.LogWarning("Hit a target with a missing ballistic material.");
                    continue;
                }

                ProcessHit(hit, target, shotDirectionNormalized, bulletMass, velocity, currentDepth, dragFactor);
                break;
            }
        }

        private static void ProcessHit(RaycastHit hit, IHitTarget target, Vector3 shotDirectionNormalized, float bulletMass, float velocity, int currentDepth, float dragFactor)
        {
            // Find (hypothetical) exit point.
            var supportPoint = hit.point + shotDirectionNormalized;
            while (hit.collider.bounds.Contains(supportPoint))
            {
                supportPoint += shotDirectionNormalized;
            }

            var allReverseHits = Physics.RaycastAll(supportPoint, -shotDirectionNormalized, (supportPoint - hit.point).magnitude * 2);
            var reverseHit = allReverseHits.First(r => r.collider == hit.collider);
            var exitPoint = reverseHit.point;
            var exitSurfaceNormal = reverseHit.normal;

            // Calculate effective impact velocity.
            var angleFactor = Mathf.Abs(Vector3.Dot(shotDirectionNormalized, hit.normal));
            var directImpactVelocity = velocity * angleFactor;

            // Deflect.
            if (directImpactVelocity < target.Material.PenetrateAtVelocity)
            {
                // Calculate the deflected bullet direction.
                var reflectedDirection = Vector3.Reflect(shotDirectionNormalized, hit.normal);
                var randomizedReflectedDirection = TransformHelper.RandomRotate(reflectedDirection, 0.4f);

                // Process the hit.
                target.Hit(new BulletImpact
                {
                    Type = BulletImpactType.Deflected,
                    EntryPoint = hit.point,
                    EntryDirectionNormalized = shotDirectionNormalized,
                    EntrySurfaceNormal = hit.normal,
                    ExitPoint = exitPoint,
                    ExitDirectionNormalized = reflectedDirection,
                    ExitSurfaceNormal = exitSurfaceNormal,
                    BulletMass = bulletMass,
                    Velocity = directImpactVelocity,
                    HitMaterial = target.Material,
                });

                // Simulate the deflected bullet, a.k.a. "ricochet".
                var reducedVelocity = (velocity - directImpactVelocity) * Mathf.Clamp(target.Material.Bouncyness, 0, 1);

                CastProjectileInternal(hit.point, randomizedReflectedDirection, bulletMass, reducedVelocity, currentDepth, dragFactor);

                return;
            }

            // Pierce.
            if (velocity > target.Material.PierceAtVelocity)
            {
                var lostVelocity = target.Material.PierceAtVelocity * dragFactor;

                // Calculate the exit bullet direction.
                var randomizedShotDirection = TransformHelper.RandomRotate(shotDirectionNormalized, 0.1f);

                target.Hit(new BulletImpact
                {
                    Type = BulletImpactType.Pierced,
                    EntryPoint = hit.point,
                    EntryDirectionNormalized = shotDirectionNormalized,
                    EntrySurfaceNormal = hit.normal,
                    ExitPoint = exitPoint,
                    ExitDirectionNormalized = randomizedShotDirection,
                    ExitSurfaceNormal = exitSurfaceNormal,
                    BulletMass = bulletMass,
                    Velocity = lostVelocity,
                    HitMaterial = target.Material,
                });

                // Simulate the bullet as it exits the target.
                var remainingVelocity = velocity - lostVelocity;

                CastProjectileInternal(exitPoint, randomizedShotDirection, bulletMass, remainingVelocity, currentDepth, dragFactor);

                return;
            }

            // Stop inside the target (i.e. absorb full energy).
            target.Hit(new BulletImpact
            {
                Type = BulletImpactType.Penetrated,
                EntryPoint = hit.point,
                EntryDirectionNormalized = shotDirectionNormalized,
                EntrySurfaceNormal = hit.normal,
                ExitPoint = exitPoint,
                ExitDirectionNormalized = shotDirectionNormalized,
                ExitSurfaceNormal = exitSurfaceNormal,
                BulletMass = bulletMass,
                Velocity = velocity,
                HitMaterial = target.Material,
            });
        }
    }
}