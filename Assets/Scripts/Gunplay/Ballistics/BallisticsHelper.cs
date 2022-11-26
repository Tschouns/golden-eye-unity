using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Performs ballistics simulations.
    /// </summary>
    public static class BallisticsHelper
    {
        /// <summary>
        /// Randomly rotates the specified vector by a random amount, no more than the maximum specified.
        /// </summary>
        /// <param name="direction">
        /// The original vector
        /// </param>
        /// <param name="maxRadians">
        /// The maximum radians by which to rotate the vector
        /// </param>
        /// <returns>
        /// A randomly rotated vector
        /// </returns>
        public static Vector3 RandomRotate(Vector3 direction, float maxRadians)
        {
            var randomVector = new Vector3(Random.value, Random.value, Random.value).normalized;
            var radians = Random.Range(0f, Mathf.Abs(maxRadians));

            var rotatedVector = Vector3.RotateTowards(direction, randomVector, radians, 0f);

            return rotatedVector;
        }

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
        public static void ShootProjectile(Vector3 origin, Vector3 direction, float bulletMass, float velocity)
        {
            var shotDirectionNormalized = direction.normalized;

            // TODO: add layer mask?
            var allRaycastHits = Physics.RaycastAll(origin, shotDirectionNormalized);

            foreach (var hit in allRaycastHits.OrderBy(r => r.distance))
            {
                var target = hit.collider.GetComponentInParent<IHitTarget>();
                if (target == null)
                {
                    Debug.LogWarning("Hit a non-target collider.");
                    continue;
                }

                if (target.Material == null)
                {
                    Debug.LogWarning("Hit a target with a missing ballistic material.");
                    continue;
                }

                ProcessHit(hit, target, shotDirectionNormalized, bulletMass, velocity);
                break;
            }
        }

        private static void ProcessHit(RaycastHit hit, IHitTarget target, Vector3 shotDirectionNormalized, float bulletMass, float velocity)
        {
            // Find (hypothetical) exit point.
            var supportPoint = hit.point + shotDirectionNormalized;
            while (hit.collider.bounds.Contains(supportPoint))
            {
                supportPoint += shotDirectionNormalized;
            }

            var allReverseHits = Physics.RaycastAll(supportPoint, -shotDirectionNormalized, (supportPoint - hit.point).magnitude);
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
                var randomizedReflectedDirection = RandomRotate(reflectedDirection, 0.4f);

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
                });

                // Simulate the deflected bullet, a.k.a. "ricochet".
                var reducedVelocity = (velocity - directImpactVelocity) * Mathf.Clamp(target.Material.Bouncyness, 0, 1);

                ShootProjectile(hit.point, randomizedReflectedDirection, bulletMass, reducedVelocity);

                return;
            }

            // Pierce.
            if (velocity > target.Material.PierceAtVelocity)
            {
                // Calculate the exit bullet direction.
                var randomizedShotDirection = RandomRotate(shotDirectionNormalized, 0.1f);

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
                    Velocity = target.Material.PierceAtVelocity,
                });                

                // Simulate the bullet as it exits the target.
                var remainingVelocity = velocity - target.Material.PierceAtVelocity;

                ShootProjectile(exitPoint, randomizedShotDirection, bulletMass, remainingVelocity);

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
            });
        }
    }
}