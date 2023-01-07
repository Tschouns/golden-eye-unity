﻿using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Continuously tries to spot an enemy. Is done as soon as an enemy has been spotted.
    /// </summary>
    public class SpotEnemy : IBehaviour
    {
        private readonly Func<float> getTimeToSpot;

        private readonly IDictionary<ICharacter, CharacterData> characters = new Dictionary<ICharacter, CharacterData>();

        public SpotEnemy(Func<float> getTimeToSpot)
        {
            Debug.Assert(getTimeToSpot != null);

            this.getTimeToSpot = getTimeToSpot;

            this.Description = $"Spot enemies.";
        }

        public string Description { get; }

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
            this.characters.Clear();
        }

        public void Update(ICharacterAccess characterAccess)
        {
            this.TrackCharactersInView(characterAccess.Perception);

            var enemies = this.characters
                .Where(c => c.Key.Faction != characterAccess.Character.Faction)
                .ToList();

            float timeToSpot = this.getTimeToSpot();
            var spottedEnemies = enemies
                .Where(c => c.Value.TimeInView > timeToSpot)
                .Select(c => c.Key)
                .ToList();

            if (spottedEnemies.Any())
            {
                this.IsDone = true;

                foreach (var enemy in spottedEnemies)
                {
                    if (!characterAccess.Memory.ActiveTargets.Contains(enemy))
                    {
                        characterAccess.Memory.ActiveTargets.Add(enemy);
                    }
                }
            }
        }

        private void TrackCharactersInView(IPerception perception)
        {
            // Track characters in view.
            foreach (var character in perception.CharactersInView)
            {
                _ = this.characters.TryAdd(
                    character,
                    new CharacterData
                    {
                        TimeInView = 0
                    });
            }

            foreach (var character in this.characters.Keys)
            {
                if (perception.CharactersInView.Contains(character))
                {
                    this.characters[character].TimeInView += Time.deltaTime;
                }
                else
                {
                    this.characters[character].TimeInView = Mathf.Max(0, this.characters[character].TimeInView - Time.deltaTime);
                }
            }
        }

        private class CharacterData
        {
            public float TimeInView { get; set; } = 0;
        }
    }
}
