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
        private AbstractSoundEmitter penetratedSoundEmitter;

        [SerializeField]
        private AbstractSoundEmitter deflectedSoundEmitter;

        [SerializeField]
        private AbstractSoundEmitter piercedSoundEmitter;

        [SerializeField]
        private GameObject[] penetratedParticleEffectPrefab;

        [SerializeField]
        private GameObject[] deflectedParticleEffectPrefab;

        [SerializeField]
        private GameObject[] piercedParticleEffectPrefab;

        public float PenetrateAtVelocity => this.penetrateAtVelocity;
        public float PierceAtVelocity => this.pierceAtVelocity;
        public float Bouncyness => this.bouncyness;
        public ISoundEmitter PenetratedSoundEmitter => this.penetratedSoundEmitter;
        public ISoundEmitter PiercedSoundEmitter => this.piercedSoundEmitter;
        public ISoundEmitter DeflectedSoundEmitter => this.deflectedSoundEmitter;
        public IEnumerable<GameObject> PenetratedParticleEffectPrefabs => this.penetratedParticleEffectPrefab;
        public IEnumerable<GameObject> PiercedParticleEffectPrefabs => this.piercedParticleEffectPrefab;
        public IEnumerable<GameObject> DeflectedParticleEffectPrefabs => this.deflectedParticleEffectPrefab;

        public void Verify()
        {
            Debug.Assert(this.penetrateAtVelocity > 0, "Penetration velocity must be greater than 0");
            Debug.Assert(this.pierceAtVelocity > this.penetrateAtVelocity, "Pierce velocity must be greater than penetration velocity");
            Debug.Assert(this.bouncyness >= 0 && this.bouncyness <= 1, "Bouncyness must be between 0 and 1");

            Debug.Assert(this.penetratedSoundEmitter != null, $"The Prefab for the {nameof(this.penetratedSoundEmitter)} must not be null!");
            Debug.Assert(this.piercedSoundEmitter != null, $"The Prefab for the {nameof(this.piercedSoundEmitter)} must not be null!");
            Debug.Assert(this.deflectedSoundEmitter != null, $"The Prefab for the {nameof(this.deflectedSoundEmitter)} must not be null!");

            Debug.Assert(this.penetratedParticleEffectPrefab != null, $"The Prefab for the {nameof(this.penetratedParticleEffectPrefab)} must not be null!");
            Debug.Assert(this.piercedParticleEffectPrefab != null, $"The Prefab for the {nameof(this.piercedParticleEffectPrefab)} must not be null!");
            Debug.Assert(this.deflectedParticleEffectPrefab != null, $"The Prefab for the {nameof(this.deflectedParticleEffectPrefab)} must not be null!");

            this.penetratedSoundEmitter.Verify();
            this.piercedSoundEmitter.Verify();
            this.deflectedSoundEmitter.Verify();

            Debug.Assert(this.penetratedParticleEffectPrefab.All(p => p != null), $"{nameof(this.penetratedParticleEffectPrefab)} must not contain null elemtents.");
            Debug.Assert(this.piercedParticleEffectPrefab.All(p => p != null), $"{nameof(this.piercedParticleEffectPrefab)} must not contain null elemtents.");
            Debug.Assert(this.deflectedParticleEffectPrefab.All(p => p != null), $"{nameof(this.deflectedParticleEffectPrefab)} must not contain null elemtents.");
        }
    }
}
