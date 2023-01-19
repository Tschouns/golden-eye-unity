using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Navigation;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration.PeacefulBehaviours
{
    public class PatrolAlongPathBehaviour : AbstractPeacefulBehaviourProvider
    {
        [SerializeField]
        private PatrolPath patrolPath;

        public override IBehaviour GetPeacefulBehaviour()
        {
            var positions = this.patrolPath.PatrolPoints.Select(p => p.Position).ToArray();

            return BehaviourFactory.CreatePatrolAlongPathBehaviour(positions);
        }

        public override void Verify()
        {
            Debug.Assert(this.patrolPath != null);
        }
    }
}