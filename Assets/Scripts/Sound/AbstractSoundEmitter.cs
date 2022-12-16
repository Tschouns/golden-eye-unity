using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public abstract class AbstractSoundEmitter : ScriptableObject, ISoundEmitter, IVerifyable
    {
        public abstract void Play(Vector3 soundOrigin);

        public abstract void Verify();
    }
}
