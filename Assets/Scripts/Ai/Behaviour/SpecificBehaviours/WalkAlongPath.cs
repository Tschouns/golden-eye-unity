using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Walks along a path specified by a list of patrol points.
    /// </summary>
    public class WalkAlongPath : IBehaviour
    {
        private readonly IBehaviour sequence;

        public WalkAlongPath(Vector3[] patrolPoints)
        {
            Debug.Assert(patrolPoints != null);

            var steps = patrolPoints.Select(p => new WalkToTarget(p)).ToArray();
            sequence = new DoInOrder(steps);
            Description = $"Walk along: {string.Join(", ", patrolPoints.Select(p => $"({p})"))}";
        }

        public string Description { get; }

        public bool IsDone => sequence.IsDone;

        public void Update(ICharacterAccess characterAccess)
        {
            sequence.Update(characterAccess);
        }

        public void Reset()
        {
            sequence.Reset();
        }
    }
}
