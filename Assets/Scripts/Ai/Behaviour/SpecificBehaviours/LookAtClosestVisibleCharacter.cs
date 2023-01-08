using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously looks the closest character in view.
    /// </summary>
    public class LookAtClosestVisibleCharacter : IBehaviour
    {
        public string Description { get; } = "Look at closest visible character.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (characterAccess.Perception.CharactersInView.Any())
            {
                var closest = characterAccess.Perception.CharactersInView
                    .OrderBy(c => Vector3.Distance(c.Head.Position, characterAccess.Character.Head.Position))
                    .First();

                characterAccess.Eyes.SetEyesFocus(closest.Head.Position);
            }
            else
            {
                characterAccess.Eyes.UnsetEyesFocus();
            }
        }
    }
}