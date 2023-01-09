using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Provides access to a character's information, such as position, visible parts, etc..
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// Gets the character's head.
        /// </summary>
        IHead Head { get; }

        /// <summary>
        /// Gets the important visible points on the character.
        /// </summary>
        IEnumerable<Vector3> VisiblePoints { get; }

        /// <summary>
        /// Gets the faction the character belongs to.
        /// </summary>
        string Faction { get; }

        /// <summary>
        /// Gets the character type which can be specified freely. E.g. a "combatant" or "civilian".
        /// </summary>
        string CharacterType { get; }

        /// <summary>
        /// Gets the character's position.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Gets a value indicating whether the character is alive.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Sets the head tilt, i.e. the head's local rotation around X, in degrees.
        /// </summary>
        /// <param name="tiltDeg">
        /// The tilt value, in degrees
        /// </param>
        void TiltHead(float tiltDeg);
    }
}
