using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously looks the closest alive target in view.
    /// </summary>
    public class LookAtClosestVisibleAliveTarget : IBehaviour
    {
        public string Description { get; } = "Look at closest alive target.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            var aliveTargetsInView = characterAccess.Perception.CharactersInView
                .Where(x => x.IsAlive && characterAccess.Memory.ActiveTargets.Contains(x))
                .ToList();

            if (aliveTargetsInView.Any())
            {
                var closest = aliveTargetsInView
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
