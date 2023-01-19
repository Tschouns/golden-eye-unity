using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Noise;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Plays the sound from the <see cref="IBallisticMaterial"/> for each <see cref="BulletImpactType"/>.
    /// </summary>
    [RequireComponent(typeof(HitTarget))]
    public class SoundHitEffect : MonoBehaviour, IHitEffect
    {
        private INoiseEventBus noiseEventBus;

        public void ReactToImpact(BulletImpact impact)
        {
            this.noiseEventBus.ProduceNoise(NoiseType.BulletImpact, impact.EntryPoint, 5);

            switch (impact.Type)
            {
                case BulletImpactType.Penetrated:
                    impact.HitMaterial.PenetratedSoundEmitter?.Play(impact.EntryPoint);
                    break;
                case BulletImpactType.Pierced:
                    impact.HitMaterial.PiercedSoundEmitter?.Play(impact.EntryPoint);
                    break;
                default:
                    impact.HitMaterial.DeflectedSoundEmitter?.Play(impact.EntryPoint);
                    break;
            }
        }

        private void Awake()
        {
            this.noiseEventBus = FindObjectOfType<NoiseEventBus>();
            Debug.Assert(this.noiseEventBus != null);
        }
    }
}
