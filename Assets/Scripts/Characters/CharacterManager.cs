
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Implements <see cref="ICharacterManager"/>.
    /// </summary>
    public class CharacterManager : MonoBehaviour, ICharacterManager
    {
        public IEnumerable<ICharacter> AllCharacters { get; private set; }

        private void Awake()
        {
            this.AllCharacters = FindObjectsOfType<Character>();
        }
    }
}
