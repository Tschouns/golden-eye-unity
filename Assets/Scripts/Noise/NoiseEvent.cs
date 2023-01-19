using UnityEngine;

namespace Assets.Scripts.Noise
{
    /// <summary>
    /// Represents a noise event happening in the world.
    /// </summary>
    public class NoiseEvent
    {
        public NoiseEvent(
            float gameTime,
            NoiseType type,
            Vector3 pointOfOrigin,
            float audibleDistance)
        {
            this.GameTime = gameTime;
            this.Type = type;
            this.PointOfOrigin = pointOfOrigin;
            this.AudibleDistance = audibleDistance;
        }

        /// <summary>
        /// Gets the (game) time the event happened.
        /// </summary>
        public float GameTime { get; }

        /// <summary>
        /// Gets the type of noise the event represents.
        /// </summary>
        public NoiseType Type { get; }

        /// <summary>
        /// Gets the point of origin.
        /// </summary>
        public Vector3 PointOfOrigin { get; }

        /// <summary>
        /// Gets the distance at which the noise is still audible.
        /// </summary>
        public float AudibleDistance { get; }
    }
}
