using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Describes a sound emitter that plays a random sound at a given position.
    /// </summary>
    [CreateAssetMenu(fileName = "RandomSoundEmitter", menuName = "Scriptable Objects/Sound/Random Emitter")]
    public class RandomSoundEmitter : AbstractSoundEmitter
    {
        [SerializeField]
        private AudioWithVolume[] clips;

        public override void Play(Vector3 soundOrigin)
        {
            var i = Random.Range(0, this.clips.Length);
            var clip = this.clips[i];

            AudioSource.PlayClipAtPoint(clip.Clip, soundOrigin, clip.Volume);
        }

        public override void Verify()
        {
            Debug.Assert(this.clips != null && this.clips.Length > 0, "No clips with volume provided.");

            foreach (var clip in this.clips)
            {
                clip.Verify();
            }
        }
    }
}
