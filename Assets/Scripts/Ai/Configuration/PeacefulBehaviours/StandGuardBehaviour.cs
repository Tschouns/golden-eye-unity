using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Behaviour.BasicBehaviours;

namespace Assets.Scripts.Ai.Configuration.PeacefulBehaviours
{
    public class StandGuardBehaviour : AbstractPeacefulBehaviourProvider
    {
        public override IBehaviour GetPeacefulBehaviour()
        {
            // TODO: save initial position, have character return to initial position.
            return new DoNothing();
        }

        public override void Verify()
        {
        }
    }
}