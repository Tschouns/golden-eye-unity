using Assets.Scripts.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Ballistics
{
    /// <summary>
    /// Represents a ballistic material. Provides all information on that material.
    /// </summary>
    public interface IBallisticMaterial
    {
        /// <summary>
        /// Gets the velocity required to penetrate the material.
        /// </summary>
        float PenetrateAtVelocity { get; }

        /// <summary>
        /// Gets the velocity required to pierce through an object of this material.
        /// </summary>
        float PierceAtVelocity { get; }

        /// <summary>
        /// Gets the "bouncyness" factor of this material, i.e. how much energy is preserved/absorbed in case of a ricochet.
        /// A value between 0 and 1.
        /// </summary>
        float Bouncyness { get; }

        /// <summary>
        /// Gets the prefabs for the bullet entry holes.
        /// </summary>
        IEnumerable<GameObject> BulletEntryHolePrefabs { get; }

        /// <summary>
        /// Gets the prefabs for the bullet exit holes.
        /// </summary>
        IEnumerable<GameObject> BulletExitHolePrefabs { get; }

        /// <summary>
        /// Gets the SoundEmitter for the "penetrated" sound.
        /// </summary>
        ISoundEmitter PenetratedSoundEmitter { get; }

        /// <summary>
        /// Gets the SoundEmitter for the "pierced" sound.
        /// </summary>
        ISoundEmitter PiercedSoundEmitter { get; }

        /// <summary>
        /// Gets the SoundEmitter for the "deflected" sound.
        /// </summary>
        ISoundEmitter DeflectedSoundEmitter { get; }

        /// <summary>
        /// Gets the prefabs for the "base" particle effects. Is always applied, no matter the hit type.
        /// </summary>
        IEnumerable<GameObject> BaseParticleEffectPrefabs { get; }

        /// <summary>
        /// Gets the prefabs for the "pierced" particle effects.
        /// </summary>
        IEnumerable<GameObject> PiercedParticleEffectPrefabs { get; }

        /// <summary>
        /// Gets the prefabs for the "deflected" particle effects.
        /// </summary>
        IEnumerable<GameObject> DeflectedParticleEffectPrefabs { get; }
    }
}
