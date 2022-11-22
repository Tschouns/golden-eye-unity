using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Represents a character's head. Provides information on the head's position and orientation.
    /// </summary>
    public interface IHead
    {
        /// <summary>
        /// Gets the head position.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Gets the position of "the eyes", i.e. the point from which the vision checks are done.
        /// </summary>
        Vector3 EyePosition { get; }

        /// <summary>
        /// Gets the head look direction.
        /// </summary>
        Vector3 LookDirection { get; }
    }
}
