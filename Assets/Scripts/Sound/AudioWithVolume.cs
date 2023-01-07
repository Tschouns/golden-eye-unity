using System;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Combines an <see cref="AudioClip"/> with a volume.
    /// </summary>
    [Serializable]
    public class AudioWithVolume
    {
        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        [Range(0f, 2f)]
        private float volume = 1f;

        public AudioClip Clip => this.clip;

        public float Volume => this.volume;
    }
}
