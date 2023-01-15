using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// "Muzzle effect" which produces particle effects.
    /// </summary>
    public class MuzzleEffect : AbstractShotEffect
    {
        private ParticleSystem[] particleSystems;

        public override void Play()
        {
            foreach (var particleSystem in this.particleSystems)
            {
                particleSystem.Play();
            }
        }

        private void Awake()
        {
            this.particleSystems = this.GetComponentsInChildren<ParticleSystem>();
        }
    }
}
