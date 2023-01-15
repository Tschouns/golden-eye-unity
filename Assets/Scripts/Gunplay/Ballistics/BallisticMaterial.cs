using Assets.Scripts.Misc;
using Assets.Scripts.Sound;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Specifies the property of a "ballistic material", i.e. a material which can be impacted by bullets.
    /// </summary>
    [CreateAssetMenu(fileName = "BallisticMaterial", menuName = "Scriptable Objects/Ballistic Material")]
    public class BallisticMaterial : ScriptableObject, IBallisticMaterial, IVerifyable
    {
        [SerializeField]
        private float penetrateAtVelocity = 100f;

        [SerializeField]
        private float pierceAtVelocity = 200f;

        [SerializeField]
        private float bouncyness = 0.5f;

        [SerializeField]
        private GameObject[] bulletEntryHolePrefabs;

        [SerializeField]
        private GameObject[] bulletExitHolePrefabs;

        [SerializeField]
        private AbstractSoundEmitter penetratedSoundEmitter;

        [SerializeField]
        private AbstractSoundEmitter deflectedSoundEmitter;

        [SerializeField]
        private AbstractSoundEmitter piercedSoundEmitter;

        [SerializeField]
        private GameObject[] baseParticleEffectPrefabs;

        [SerializeField]
        private GameObject[] deflectedParticleEffectPrefabs;

        [SerializeField]
        private GameObject[] piercedParticleEffectPrefabs;

        public float PenetrateAtVelocity => this.penetrateAtVelocity;
        public float PierceAtVelocity => this.pierceAtVelocity;
        public float Bouncyness => this.bouncyness;
        public IEnumerable<GameObject> BulletEntryHolePrefabs => this.bulletEntryHolePrefabs;
        public IEnumerable<GameObject> BulletExitHolePrefabs => this.bulletExitHolePrefabs;
        public ISoundEmitter PenetratedSoundEmitter => this.penetratedSoundEmitter;
        public ISoundEmitter PiercedSoundEmitter => this.piercedSoundEmitter;
        public ISoundEmitter DeflectedSoundEmitter => this.deflectedSoundEmitter;
        public IEnumerable<GameObject> BaseParticleEffectPrefabs => this.baseParticleEffectPrefabs;
        public IEnumerable<GameObject> PiercedParticleEffectPrefabs => this.piercedParticleEffectPrefabs;
        public IEnumerable<GameObject> DeflectedParticleEffectPrefabs => this.deflectedParticleEffectPrefabs;

        public void Verify()
        {
            Debug.Assert(this.penetrateAtVelocity > 0, "Penetration velocity must be greater than 0");
            Debug.Assert(this.pierceAtVelocity > this.penetrateAtVelocity, "Pierce velocity must be greater than penetration velocity");
            Debug.Assert(this.bouncyness >= 0 && this.bouncyness <= 1, "Bouncyness must be between 0 and 1");

            // Bullet holes.
            Debug.Assert(this.bulletEntryHolePrefabs != null, $"{nameof(this.bulletEntryHolePrefabs)} must not be null!");
            Debug.Assert(this.bulletExitHolePrefabs != null, $"{nameof(this.bulletExitHolePrefabs)} must not be null!");

            Debug.Assert(this.bulletEntryHolePrefabs.All(p => p != null), $"{nameof(this.bulletEntryHolePrefabs)} must not contain null elemtents.");
            Debug.Assert(this.bulletExitHolePrefabs.All(p => p != null), $"{nameof(this.bulletExitHolePrefabs)} must not contain null elemtents.");

            // Sound emitters.
            Debug.Assert(this.penetratedSoundEmitter != null, $"{nameof(this.penetratedSoundEmitter)} must not be null!");
            Debug.Assert(this.piercedSoundEmitter != null, $"{nameof(this.piercedSoundEmitter)} must not be null!");
            Debug.Assert(this.deflectedSoundEmitter != null, $"{nameof(this.deflectedSoundEmitter)} must not be null!");

            this.penetratedSoundEmitter.Verify();
            this.piercedSoundEmitter.Verify();
            this.deflectedSoundEmitter.Verify();

            // Particle effects.
            Debug.Assert(this.baseParticleEffectPrefabs != null, $"{nameof(this.baseParticleEffectPrefabs)} must not be null!");
            Debug.Assert(this.piercedParticleEffectPrefabs != null, $"{nameof(this.piercedParticleEffectPrefabs)} must not be null!");
            Debug.Assert(this.deflectedParticleEffectPrefabs != null, $"{nameof(this.deflectedParticleEffectPrefabs)} must not be null!");

            Debug.Assert(this.baseParticleEffectPrefabs.All(p => p != null), $"{nameof(this.baseParticleEffectPrefabs)} must not contain null elemtents.");
            Debug.Assert(this.piercedParticleEffectPrefabs.All(p => p != null), $"{nameof(this.piercedParticleEffectPrefabs)} must not contain null elemtents.");
            Debug.Assert(this.deflectedParticleEffectPrefabs.All(p => p != null), $"{nameof(this.deflectedParticleEffectPrefabs)} must not contain null elemtents.");
        }
    }
}
