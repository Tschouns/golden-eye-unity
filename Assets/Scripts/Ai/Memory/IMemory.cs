
using Assets.Scripts.Ai.Navigation;
using Assets.Scripts.Characters;
using Assets.Scripts.Noise;
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
        /// Gets all the noise events heard in the past.
        /// </summary>
        IList<NoiseEvent> NoisesHeard { get; }

        /// <summary>
        /// Gets the NPC's current active targets.
        /// </summary>
        IReadOnlyCollection<ICharacter> ActiveTargets { get; }

        ///// <summary>
        ///// Adds a noise event to the memory.
        ///// </summary>
        ///// <param name="noiseEvent">
        ///// The noise event
        ///// </param>
        //void AddNoiseEvent(NoiseEvent noiseEvent);

        /// <summary>
        /// Tries to adds an active target to the memory.
        /// </summary>
        /// <param name="activeTarget">
        /// The active target
        /// </param>
        /// <returns>
        /// A value indicating whether the target was actually added
        /// </returns>
        bool TryAddActiveTarget(ICharacter activeTarget);

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