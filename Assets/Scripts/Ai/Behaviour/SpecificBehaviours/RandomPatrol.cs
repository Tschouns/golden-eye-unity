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

            Description = $"Random patrol among {string.Join(", ", patrolPoints.Select(p => $"({p})"))}";
        }

        public string Description { get; }

        public bool IsDone => false;

        public void Update(ICharacterAccess characterAccess)
        {
            // Still persuing target?
            if (currentTargetPoint != null &&
                Vector3.Distance(currentTargetPoint.Value, characterAccess.Character.Position) > 2f)
            {
                return;
            }

            // Select a new target.
            currentTargetPoint = SelectNextDestinationPoint();
            characterAccess.WalkTo(currentTargetPoint.Value);
        }

        public void Reset()
        {
            currentTargetPoint = null;
        }

        private Vector3 SelectNextDestinationPoint()
        {
            var newTargetIndex = Random.Range(0, patrolPoints.Length - 1);
            return patrolPoints[newTargetIndex];
        }
    }
}
