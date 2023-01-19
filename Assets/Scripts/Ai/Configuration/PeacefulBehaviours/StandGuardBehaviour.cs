using Assets.Scripts.Ai.Behaviour;

namespace Assets.Scripts.Ai.Configuration.PeacefulBehaviours
{
    public class StandGuardBehaviour : AbstractPeacefulBehaviourProvider
    {

        public override IBehaviour GetPeacefulBehaviour()
        {
            return BehaviourFactory.CreateStandGuardAtPositionBehaviour(this.transform.position, this.transform.forward);
        }

        public override void Verify()
        {
        }
    }
}