using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    public class RunTowardsClosestTarget : IBehaviour
    {
        public string Description => "Run towards closest target.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (!characterAccess.Memory.ActiveTargets.Any())
            {
                this.IsDone = true;
                return;
            }

            var closestTarget = characterAccess.Memory.ActiveTargets
                .OrderBy(c => Vector3.Distance(c.Head.Position, characterAccess.Character.Head.Position))
                .First();

            characterAccess.RunTo(closestTarget.Position);
        }
    }
}