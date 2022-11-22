using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Continuously patrols a long a path -- forth and back.
    /// </summary>
    public class PatrolEndToEnd : IBehaviour
    {
        private readonly IBehaviour cycle;

        public PatrolEndToEnd(Vector3[] patrolPoints)
        {
            Debug.Assert(patrolPoints != null);

            var walkStartToEnd = new WalkAlongPath(patrolPoints);
            var walkEndToStart = new WalkAlongPath(patrolPoints.Reverse().ToArray());

            cycle = new CycleThrough(walkStartToEnd, walkEndToStart);
            Description = $"Patrol between ({patrolPoints.FirstOrDefault()}) and ({patrolPoints.LastOrDefault()})";
        }

        public string Description { get; }

        public bool IsDone => false;

        public void Update(ICharacterAccess characterAccess)
        {
            cycle.Update(characterAccess);
        }

        public void Reset()
        {
            cycle.Reset();
        }
    }
}
