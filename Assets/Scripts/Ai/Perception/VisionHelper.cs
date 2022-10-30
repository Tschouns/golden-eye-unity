using Assets.Scripts.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Perception
{
    /// <summary>
    /// Provides helper methods for simulating a character's vision.
    /// </summary>
    public static class VisionHelper
    {
        public static IEnumerable<ICharacter> CheckForVisibleCharacters(IHead head, IEnumerable<ICharacter> allCharacters, float fieldOfViewDegrees)
        {
            Debug.Assert(head != null);
            Debug.Assert(allCharacters != null);

            var visibleCharacters = allCharacters.Where(c => CheckIsCharacterVisible(head, c, fieldOfViewDegrees)).ToList();

            return visibleCharacters;
        }

        public static bool CheckIsCharacterVisible(IHead head, ICharacter otherCharacter, float fieldOfViewDegrees)
        {
            Debug.Assert(head != null);
            Debug.Assert(otherCharacter != null);

            // Special case: the head belongs to the character itself.
            if (head == otherCharacter.Head)
            {
                return false;
            }

            var otherCharacterDirection = otherCharacter.Head.Position - head.EyePosition;

            // Check field of view.
            var otherCharacterOffsetAngle = Vector3.Angle(head.LookDirection, otherCharacterDirection);

            if (Mathf.Abs(otherCharacterOffsetAngle) > Mathf.Abs(fieldOfViewDegrees / 2))
            {
                return false;
            }

            // Check for obstructions.
            if (Physics.Raycast(head.EyePosition, otherCharacterDirection, out var hit, otherCharacterDirection.magnitude))
            {
                var character = hit.collider.GetComponentInParent<ICharacter>();
                if (character == null)
                {
                    // Ray has hit an obstacle.
                    return false;
                }

                return character == otherCharacter;
            }
            else
            {
                return false;
            }
        }
    }
}