using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Describes a bullet impact.
    /// </summary>
    public struct BulletImpact
    {
        /// <summary>
        /// Gets or sets the bullet impact type.
        /// </summary>
        public BulletImpactType Type { get; set; }

        /// <summary>
        /// Gets or sets the bullet entry point.
        /// </summary>
        public Vector3 EntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the hit direction -- as a normalized vector.
        /// </summary>
        public Vector3 DirectionNormalized { get; set; }

        /// <summary>
        /// Gets or sets the surface normal.
        /// </summary>
        public Vector3 SurfaceNormal { get; set; }

        /// <summary>
        /// Gets or sets the (hypothetical) bullet exit point, if the bullet were to pierce the target in a straight line.
        /// </summary>
        public Vector3 ExitPoint { get; set; }

        /// <summary>
        /// Gets or sets the bullet mass.
        /// </summary>
        public float BulletMass { get; set; }

        /// <summary>
        /// Gets or sets the bullet impact velocity.
        /// </summary>
        public float Velocity { get; set; }

        /// <summary>
        /// Calculates the impulse vector for the impact, defined as J = mv2 - mv1.
        /// </summary>
        /// <returns>
        /// An impulse vector
        /// </returns>
        public Vector3 GetImpulse()
        {
            return this.DirectionNormalized * this.Velocity * this.BulletMass;
        }

        /// <summary>
        /// Calculates the kinetic energy delivered with the impact, defines as E = (mv^2) / 2.
        /// </summary>
        /// <returns>
        /// The kinetic energy
        /// </returns>
        public float GetKineticEnergy()
        {
            return this.BulletMass * this.Velocity * this.Velocity * 0.5f;
        }
    }
}
