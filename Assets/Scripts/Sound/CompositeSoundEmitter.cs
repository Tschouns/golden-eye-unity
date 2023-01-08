using System;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Allows to combine multiple sound emitters into one.
    /// </summary>
    [CreateAssetMenu(fileName = "CompositeSoundEmitter", menuName = "Scriptable Objects/Sound/Composite Emitter")]
    public class CompositeSoundEmitter : AbstractSoundEmitter
    {
        [SerializeField]
        private AbstractSoundEmitter[] emitters;

        public override void Play(Vector3 soundOrigin)
        {
            Array.ForEach(this.emitters, emitter => emitter.Play(soundOrigin));
        }

        public override void Verify()
        {
            Array.ForEach(this.emitters, emitter => emitter.Verify());
        }
    }
}
