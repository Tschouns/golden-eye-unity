using Assets.Scripts.Ai.Navigation;
using Assets.Scripts.Characters;
using Assets.Scripts.Noise;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ai.Memory
{
    /// <summary>
    /// Implements <see cref="IMemory"/>.
    /// </summary>
    public class MemoryImpl : IMemory
    {
        private readonly List<NoiseEvent> noiseEvents = new List<NoiseEvent>();
        private readonly HashSet<ICharacter> activeTargets = new HashSet<ICharacter>();
        private readonly IDictionary<ICharacter, CharacterInfo> characterInfos = new Dictionary<ICharacter, CharacterInfo>();
        private readonly IEscapePointProvider escapePointProvider;

        public MemoryImpl(IEscapePointProvider escapePointProvider)
        {
            Debug.Assert(escapePointProvider != null);

            this.escapePointProvider = escapePointProvider;
        }

        public IEnumerable<IEscapePoint> EscapePoints => this.escapePointProvider.EscapePoints;
        public IList<NoiseEvent> NoisesHeard => this.noiseEvents;
        public IReadOnlyCollection<ICharacter> ActiveTargets => this.activeTargets;

        //public void AddNoiseEvent(NoiseEvent noiseEvent)
        //{
        //    Debug.Assert(noiseEvent != null);

        //    this.noiseEvents.Add(noiseEvent);
        //}

        public bool TryAddActiveTarget(ICharacter activeTarget)
        {
            Debug.Assert(activeTarget != null);

            if (!this.activeTargets.Contains(activeTarget))
            {
                this.activeTargets.Add(activeTarget);
                return true;
            }

            return false;
        }

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