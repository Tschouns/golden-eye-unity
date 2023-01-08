using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Continously patrols among a set of patrol points, randomly selecting the next point each time a point is reached.
    /// </summary>
    public class RandomPatrol : IBehaviour
    {
        private readonly Vector3[] patrolPoints;

        private Vector3? currentTargetPoint;

        public RandomPatrol(
            Vector3[] patrolPoints)
        {
            Debug.Assert(patrolPoints != null);

            this.patrolPoints = patrolPoints;

            this.Description = $"Random patrol among {string.Join(", ", patrolPoints.Select(p => $"({p})"))}";
        }

        public string Description { get; }

        public bool IsDone => false;

        public void Update(ICharacterAccess characterAccess)
        {
            // Still persuing target?
            if (this.currentTargetPoint != null &&
                Vector3.Distance(this.currentTargetPoint.Value, characterAccess.Character.Position) > 2f)
            {
                return;
            }

            // Select a new target.
            this.currentTargetPoint = this.SelectNextDestinationPoint();
            characterAccess.WalkTo(this.currentTargetPoint.Value);
        }

        public void Reset()
        {
            this.currentTargetPoint = null;
        }

        private Vector3 SelectNextDestinationPoint()
        {
            var newTargetIndex = Random.Range(0, this.patrolPoints.Length - 1);
            return this.patrolPoints[newTargetIndex];
        }
    }
}
