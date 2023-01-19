
using Assets.Scripts.Characters;
using Assets.Scripts.Noise;
using System.Collections.Generic;

namespace Assets.Scripts.Ai.Perception
{
    /// <summary>
    /// Represents an NPC's perception. Provides access to what an NPC sees and hears.
    /// </summary>
    public interface IPerception
    {
        /// <summary>
        /// Gets all the characters currently in this character's view.
        /// </summary>
        IEnumerable<ICharacter> CharactersInView { get; }

        /// <summary>
        /// Tries to deque a noise event.
        /// </summary>
        /// <param name="noise">
        /// The noise event, if there is one; otherwise null
        /// </param>
        /// <returns>
        /// A value indicating whether a noise event has found
        /// </returns>
        bool TryDequeNoise(out NoiseEvent noise);
    }
}
