using UnityEngine;

namespace Assets.Scripts.Sound
{
    /// <summary>
    /// Abstracts an <see cref="AudioSource"/> that can simple be played at a given position.
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
