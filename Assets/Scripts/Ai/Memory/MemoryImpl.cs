using Assets.Scripts.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ai.Memory
{
    /// <summary>
    /// Implements <see cref="IMemory"/>.
    /// </summary>
    public class MemoryImpl : IMemory
    {
        private readonly IDictionary<ICharacter, CharacterInfo> characterInfos = new Dictionary<ICharacter, CharacterInfo>();

        public ICollection<ICharacter> ActiveTargets { get; } = new HashSet<ICharacter>();

        public CharacterInfo GetKnownInfoOnCharacter(ICharacter character)
        {
            Debug.Assert(character != null);

            if (!this.characterInfos.ContainsKey(character))
            {
                this.characterInfos.Add(character, new CharacterInfo());
            }

            return this.characterInfos[character];
        }
    }
}