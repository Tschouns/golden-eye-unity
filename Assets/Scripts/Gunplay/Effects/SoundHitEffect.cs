using Assets.Scripts.Gunplay.Ballistics;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Plays the sound from the <see cref="IBallisticMaterial"/> for each <see cref="BulletImpactType"/>.
    /// </summary>
    [RequireComponent(typeof(HitTarget))]
    public class SoundHitEffect : MonoBehaviour, IHitEffect
    {
        public void ReactToImpact(BulletImpact impact)
        {
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
    }
}
