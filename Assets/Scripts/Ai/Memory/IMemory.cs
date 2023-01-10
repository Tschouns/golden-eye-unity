
using Assets.Scripts.Ai.Navigation;
using Assets.Scripts.Characters;
using System.Collections.Generic;

namespace Assets.Scripts.Ai.Memory
{
    /// <summary>
    /// Represents an NPC's memory. Can keep track of other character's in the world.
    /// </summary>
    public interface IMemory
    {
        /// <summary>
        /// Gets all the available escape points known to the NPC.
        /// </summary>
        IEnumerable<IEscapePoint> EscapePoints { get; }

        /// <summary>
        /// Gets the NPC's current active targets.
        /// </summary>
        ICollection<ICharacter> ActiveTargets { get; }

        /// <summary>
        /// Gets the known information on the specified character.
        /// </summary>
        /// <param name="character">
        /// The character for which to retrieve the info
        /// </param>
        /// <returns>
        /// The info on the character
        /// </returns>
        CharacterInfo GetKnownInfoOnCharacter(ICharacter character);
    }
}