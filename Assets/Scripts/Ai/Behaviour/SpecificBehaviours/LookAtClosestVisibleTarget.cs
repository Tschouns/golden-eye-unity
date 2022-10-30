using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously looks the closest active target in view.
    /// </summary>
    public class LookAtClosestVisibleTarget : IBehaviour
    {
        public string Description { get; } = "Look at closest visible target.";

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
                    .First(c => c != characterAccess.Character);

                characterAccess.LookAt(closest.Position);
            }
            else
            {
                characterAccess.LookAhead();
            }
        }
    }
}