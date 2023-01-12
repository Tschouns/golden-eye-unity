using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Implements a patrol path. Collects control points from its child objects.
    /// </summary>
    public class PatrolPath : MonoBehaviour, IPatrolPath
    {
        private IPatrolPoint[] patrolPoints;

        public IReadOnlyList<IPatrolPoint> PatrolPoints => this.patrolPoints ?? this.LoadPatrolPoints();

        private IReadOnlyList<IPatrolPoint> LoadPatrolPoints()
        {
            this.patrolPoints = this.GetComponentsInChildren<IPatrolPoint>();

            return this.patrolPoints;
        }
    }
}