using Assets.Scripts.Ai.Memory;
using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour
{
    /// <summary>
    /// Provides an interface for AI components to interact with and control the character.
    /// </summary>
    public interface ICharacterAccess
    {
        /// <summary>
        /// Gets the character itself.
        /// </summary>
        ICharacter Character { get; }

        /// <summary>
        /// Gets access to the character's eyes.
        /// </summary>
        IEyes Eyes { get; }

        /// <summary>
        /// Gets the character's perception.
        /// </summary>
        IPerception Perception { get; }

        /// <summary>
        /// Gets the character's memory.
        /// </summary>
        IMemory Memory { get; }

        /// <summary>
        /// Gets the character's gun handler.
        /// </summary>
        IGunHandler GunHandler { get; }

        /// <summary>
        /// Gets the character's current destination.
        /// </summary>
        Vector3 CurrentDestination { get; }

        /// <summary>
        /// Makes the character walk to the specified destination.
        /// </summary>
        /// <param name="destination">
        /// The desination
        /// </param>
        void WalkTo(Vector3 destination);

        /// <summary>
        /// Makes the character run to the specified destination.
        /// </summary>
        /// <param name="destination">
        /// The desination
        /// </param>
        void RunTo(Vector3 destination);

        /// <summary>
        /// Makes the character turn towards the specified point.
        /// </summary>
        /// <param name="targetPoint">
        /// The point to look at
        /// </param>
        void TurnTowardsPoint(Vector3 targetPoint);

        /// <summary>
        /// Makes the character turn ahead, i.e. in moving direction.
        /// </summary>
        void TurnAhead();
    }
}
