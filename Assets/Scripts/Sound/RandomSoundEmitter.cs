using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Describes a sound emitter that plays a random sound at a given position.
    /// </summary>
    [CreateAssetMenu(
        fileName = "RandomSoundEmitter",
        menuName = "Scriptable Objects/Sound/Random Emitter"
    )]
    public class RandomSoundEmitter : AbstractSoundEmitter
    {
        [SerializeField]
        private AudioWithVolume[] clips;

        public override void Play(Vector3 soundOrigin)
        {
            var clip = this.clips[Random.Range(0, this.clips.Length)];
            AudioSource.PlayClipAtPoint(clip.Clip, soundOrigin, clip.Volume);
        }

        public override void Verify()
        {
            Debug.Assert(clips != null && clips.Length > 0, "No clips with volume provided!");
            Debug.Assert(System.Array.TrueForAll(clips, a => a.Clip != null), "No clip provided!");
            Debug.Assert(System.Array.TrueForAll(clips, a => a.Volume >= 0f && a.Volume <= 1f),"Clip must have Volume between 0 and 1!");
        }
    }
}
