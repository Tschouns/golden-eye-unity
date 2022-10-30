
using Assets.Scripts.Characters;
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
    }
}
