using Assets.Scripts.Ai.Navigation;
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
        private readonly IEscapePointProvider escapePointProvider;

        public MemoryImpl(IEscapePointProvider escapePointProvider)
        {
            Debug.Assert(escapePointProvider != null);

            this.escapePointProvider = escapePointProvider;
        }

        public IEnumerable<IEscapePoint> EscapePoints => this.escapePointProvider.EscapePoints;

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