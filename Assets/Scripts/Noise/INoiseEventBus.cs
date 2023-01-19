
using System;
using UnityEngine;

namespace Assets.Scripts.Noise
{
    /// <summary>
    /// Represents an event bus for noise events.
    /// </summary>
    public interface INoiseEventBus
    {
        /// <summary>
        /// Is fired when a noise event happens in the world.
        /// </summary>
        public event Action<NoiseEvent> OnNoise;

        /// <summary>
        /// Produces a noise event in the world.
        /// </summary>
        /// <param name="type">
        /// Gets the type of noise the event represents.
        /// </param>
        /// <summary>
        /// Gets the point of origin.
        /// </summary>
        /// <summary>
        /// Gets the distance at which the noise is still audible.
        /// </summary>
        public void ProduceNoise(
            NoiseType type,
            Vector3 pointOfOrigin,
            float audibleDistance);
    }
}
