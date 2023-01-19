using Assets.Scripts.Characters;
using Assets.Scripts.Noise;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Perception
{
    /// <summary>
    /// Implements <see cref="IPerception"/>.
    /// </summary>
    public class PerceptionImpl : IPerception
    {
        private readonly ICharacter character;
        private readonly ICharacterManager characterManager;
        private readonly Func<float> getFieldOfView;

        private NoiseEvent currentNoiseEvent;

        public PerceptionImpl(
            ICharacter character,
            INoiseEventBus noiseEventBus,
            ICharacterManager characterManager,
            Func<float> getFieldOfView)
        {
            Debug.Assert(character != null);
            Debug.Assert(noiseEventBus != null);
            Debug.Assert(characterManager != null);
            Debug.Assert(getFieldOfView != null);

            this.character = character;
            this.characterManager = characterManager;
            this.getFieldOfView = getFieldOfView;

            noiseEventBus.OnNoise += e =>
            {
                if (Vector3.Distance(this.character.Head.Position, e.PointOfOrigin) < e.AudibleDistance)
                {
                    this.currentNoiseEvent = e;
                }
            };
        }

        public IEnumerable<ICharacter> CharactersInView { get; private set; } = new List<ICharacter>();

        public bool TryDequeNoise(out NoiseEvent noise)
        {
            if (this.currentNoiseEvent == null)
            {
                noise = null;

                return false;
            }

            noise = this.currentNoiseEvent;
            this.currentNoiseEvent = null;

            return true;
        }

        public void Update()
        {
            //if (this.currentNoiseEvent != null &&
            //    Vector3.Distance(this.character.Head.Position, this.currentNoiseEvent.PointOfOrigin) < this.currentNoiseEvent.AudibleDistance)
            //{
            //    this.currentNoiseEvent = null;
            //}

            var allOtherCharacters = this.characterManager.AllCharacters.Where(c => c != this.character).ToList();

            this.CharactersInView = VisionHelper.CheckForVisibleCharacters(
                this.character.Head,
                allOtherCharacters,
                this.getFieldOfView());
        }
    }
}
