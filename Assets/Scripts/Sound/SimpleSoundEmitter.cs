using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Describes a sound emitter that plays a single sound at a given position.
    /// </summary>
    [CreateAssetMenu(
        fileName = "SimpleSoundEmitter",
        menuName = "Scriptable Objects/Sound/Simple Emitter"
    )]
    public class SimpleSoundEmitter : AbstractSoundEmitter
    {
        [SerializeField]
        private AudioWithVolume audio;

        public override void Play(Vector3 soundOrigin)
        {
            AudioSource.PlayClipAtPoint(this.audio.Clip, soundOrigin, this.audio.Volume);
        }

        public override void Verify()
        {
            Debug.Assert(this.audio.Clip != null, "No clip provided!");
            Debug.Assert(this.audio.Volume >= 0f && this.audio.Volume <= 1f, "Clip must have Volume between 0 and 1!");
        }
    }
}
