using UnityEngine;
using System;

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
        [Range(0f, 1f)]
        private float volume = 1f;

        public AudioClip Clip => clip;

        public float Volume => volume;
    }
}
