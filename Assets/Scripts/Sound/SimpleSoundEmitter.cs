using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    [CreateAssetMenu(fileName = "SimpleSoundEmitter", menuName = "Scriptable Objects/Simple Sound Emitter")]
    public class SimpleSoundEmitter : ScriptableObject, IVerifyable
    {
        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        private float volume = 1f;

        public void Play(Vector3 soundOrigin)
        {
            AudioSource.PlayClipAtPoint(this.clip, soundOrigin, this.volume);
        }

        public void Verify()
        {
            Debug.Assert(clip != null);
            Debug.Assert(volume >= 0f);
        }
    }
}