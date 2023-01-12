using Assets.Scripts.Misc;
using System;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Combines an <see cref="AudioClip"/> with a volume.
    /// </summary>
    [Serializable]
    public class AudioWithVolume : IVerifyable
    {
        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 1f;

        public AudioClip Clip => this.clip;

        public float Volume => this.volume;

        public void Verify()
        {
            Debug.Assert(this.Clip != null, "No clip provided.");
            Debug.Assert(this.Volume > 0f, "Volume must be greater than 0.");
            Debug.Assert(this.Volume <= 1f, "Volume must be no greater than 1.");
        }
    }
}
