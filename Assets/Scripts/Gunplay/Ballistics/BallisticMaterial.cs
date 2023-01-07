using Assets.Scripts.Misc;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Specifies the property of a "ballistic material", i.e. a material which can be impacted by bullets.
    /// </summary>
    [CreateAssetMenu(
        fileName = "BallisticMaterial",
        menuName = "Scriptable Objects/Ballistic Material"
    )]
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
        private GameObject[] penetratedParticleEffectPrefab = new GameObject[1];

        [SerializeField]
        private GameObject[] deflectedParticleEffectPrefab = new GameObject[1];

        [SerializeField]
        private GameObject[] piercedParticleEffectPrefab = new GameObject[1];

        public float PenetrateAtVelocity => this.penetrateAtVelocity;
        public float PierceAtVelocity => this.pierceAtVelocity;
        public float Bouncyness => this.bouncyness;
        public ISoundEmitter PenetratedSoundEmitter => this.penetratedSoundEmitter;
        public ISoundEmitter PiercedSoundEmitter => this.piercedSoundEmitter;
        public ISoundEmitter DeflectedSoundEmitter => this.deflectedSoundEmitter;

        public GameObject[] PenetratedParticleEffectPrefab => this.penetratedParticleEffectPrefab;

        public GameObject[] PiercedParticleEffectPrefab => this.piercedParticleEffectPrefab;

        public GameObject[] DeflectedParticleEffectPrefab => this.deflectedParticleEffectPrefab;

        public void Verify()
        {
            Debug.Assert(this.penetrateAtVelocity > 0, "Penetration velocity must be greater than 0");
            Debug.Assert(this.pierceAtVelocity > this.penetrateAtVelocity, "Pierce velocity must be greater than penetration velocity");
            Debug.Assert(this.bouncyness >= 0 && this.bouncyness <= 1, "Bouncyness must be between 0 and 1");
            this.penetratedSoundEmitter.Verify();
            this.piercedSoundEmitter.Verify();
            this.deflectedSoundEmitter.Verify();
            Debug.Assert(this.penetratedParticleEffectPrefab != null, "The Prefab for the 'Penetrated Particle Effect' must not be null!");
            Debug.Assert(this.piercedParticleEffectPrefab != null, "The Prefab for the 'Pierced Particle Effect' must not be null!");
            Debug.Assert(this.deflectedParticleEffectPrefab != null, "The Prefab for the 'Deflected Particle Effect' must not be null!");
        }
    }
}
