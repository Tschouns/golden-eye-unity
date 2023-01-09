using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Abstracts an sound source that can simply be played at a given position.
    /// </summary>
    public interface ISoundEmitter
    {
        /// <summary>
        /// Plays a sound at the given position.
        /// </summary>
        /// <param name="soundOrigin">The Point to play the sound at</param>
        void Play(Vector3 soundOrigin);
    }
}
