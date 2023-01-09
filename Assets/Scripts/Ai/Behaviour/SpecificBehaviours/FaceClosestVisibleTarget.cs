using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously faces the closest active target in view.
    /// </summary>
    public class FaceClosestVisibleTarget : IBehaviour
    {
        public string Description { get; } = "Face closest visible target.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            var visibleTargets = characterAccess.Memory.ActiveTargets
                .Where(t => characterAccess.Perception.CharactersInView.Contains(t))
                .ToList();

            if (visibleTargets.Any())
            {
                var closest = visibleTargets
                    .OrderBy(c => Vector3.Distance(c.Head.Position, characterAccess.Character.Head.Position))
                    .First();

                characterAccess.TurnTowardsPoint(closest.Head.Position);
                characterAccess.Eyes.SetEyesFocus(closest.Head.Position);
            }
            else
            {
                characterAccess.TurnAhead();
                characterAccess.Eyes.UnsetEyesFocus();
            }
        }
    }
}