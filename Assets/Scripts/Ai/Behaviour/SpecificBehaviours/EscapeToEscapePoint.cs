using Assets.Scripts.Ai.Navigation;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character escapes to an escape point.
    /// </summary>
    public class EscapeToEscapePoint : IBehaviour
    {
        private IEscapePoint currentEscapeTarget;

        public string Description { get; } = "Escape to escape point.";

        public bool IsDone { get; private set; }

        public void Reset()
        {
            this.IsDone = false;
            this.currentEscapeTarget = null;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (this.IsDone)
            {
                return;
            }

            if (this.currentEscapeTarget == null)
            {
                if (!characterAccess.Memory.EscapePoints.Any())
                {
                    return;
                }

                // Find an escape point -- prefereably the second closest, in case they have to escape repeatedly.
                var pointsByDistance = characterAccess.Memory.EscapePoints
                    .OrderBy(p => Vector3.Distance(p.Position, characterAccess.Character.Position))
                    .ToList();

                if (pointsByDistance.Count > 1)
                {
                    this.currentEscapeTarget = pointsByDistance.Skip(1).First();
                }
                else
                {
                    this.currentEscapeTarget = pointsByDistance.First();
                }

                characterAccess.RunTo(this.currentEscapeTarget.Position);
            }

            if (Vector3.Distance(characterAccess.Character.Position, this.currentEscapeTarget.Position) < 2)
            {
                characterAccess.Stop();
                this.IsDone = true;
            }
        }
    }
}