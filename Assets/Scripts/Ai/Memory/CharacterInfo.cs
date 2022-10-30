using UnityEngine;

namespace Assets.Scripts.Ai.Memory
{
    /// <summary>
    /// Contains information regarding a specific character.
    /// </summary>
    public class CharacterInfo
    {
        /// <summary>
        /// Gets the character's last known position.
        /// </summary>
        public Vector3? LastKnownPosition { get; set; } = null;

        /// <summary>
        /// Gets a value indicating whether the character was alive when last seen.
        /// </summary>
        public bool WasAliveWhenLastSeen { get; set; } = true;
    }
}