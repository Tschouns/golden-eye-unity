using Assets.Scripts.Damage;
using Assets.Scripts.Gunplay.Ballistics;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Plays sounds from the <see cref="IBallisticMaterial"/>, only while the hit target is alive.
    /// </summary>
    [RequireComponent(typeof(HitTarget))]
    public class SoundWhileAliveHitEffect : MonoBehaviour, IHitEffect, INotifyOnDied
    {
        private bool isAlive = true;

        public void ReactToImpact(BulletImpact impact)
        {
            if (!this.isAlive)
            {
                return;
            }

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

        public void NotifyOnDied()
        {
            this.isAlive = false;
        }
    }
}