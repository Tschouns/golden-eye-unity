using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Noise;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Produces noise events which AI can hear.
    /// </summary>
    /// <remarks>
    /// Only place on objects which exist at the start of the scene, not the ones spawned in!!
    /// </remarks>
    [RequireComponent(typeof(HitTarget))]
    public class NoiseHitEffect : MonoBehaviour, IHitEffect
    {
        private INoiseEventBus noiseEventBus;

        public void ReactToImpact(BulletImpact impact)
        {
            this.noiseEventBus.ProduceNoise(NoiseType.BulletImpact, impact.EntryPoint, 5);
        }

        private void Awake()
        {
            this.noiseEventBus = FindObjectOfType<NoiseEventBus>();
            Debug.Assert(this.noiseEventBus != null);
        }
    }
}
