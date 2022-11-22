using Assets.Scripts.Characters;
using System;
using System.Collections.Generic;
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

        public PerceptionImpl(
            ICharacter character,
            ICharacterManager characterManager,
            Func<float> getFieldOfView)
        {
            Debug.Assert(character != null);
            Debug.Assert(characterManager != null);
            Debug.Assert(getFieldOfView != null);

            this.character = character;
            this.characterManager = characterManager;
            this.getFieldOfView = getFieldOfView;
        }

        public IEnumerable<ICharacter> CharactersInView { get; private set; } = new List<ICharacter>();

        public void Update()
        {
            this.CharactersInView = VisionHelper.CheckForVisibleCharacters(
                this.character.Head,
                this.characterManager.AllCharacters,
                this.getFieldOfView());
        }
    }
}
