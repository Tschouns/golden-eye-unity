
using System.Collections.Generic;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Tracks all existing characters in a scene.
    /// </summary>
    public interface ICharacterManager
    {
        /// <summary>
        /// Gets all the characters in the scene.
        /// </summary>
        IEnumerable<ICharacter> AllCharacters { get; }
    }
}
