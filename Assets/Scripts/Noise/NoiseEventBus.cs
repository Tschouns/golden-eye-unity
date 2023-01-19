
using System;
using UnityEngine;

namespace Assets.Scripts.Noise
{
    public class NoiseEventBus : MonoBehaviour, INoiseEventBus
    {
        public event Action<NoiseEvent> OnNoise;

        public void ProduceNoise(NoiseType type, Vector3 pointOfOrigin, float audibleDistance)
        {
            var noiseEvent = new NoiseEvent(
                Time.time,
                type,
                pointOfOrigin,
                audibleDistance);

            this.OnNoise?.Invoke(noiseEvent);
        }
    }
}
