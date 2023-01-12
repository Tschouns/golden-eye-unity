using Assets.Scripts.Ai.Behaviour;

namespace Assets.Scripts.Ai.Configuration.AlertBehaviours
{
    /// <summary>
    /// Provides an "alert" behaviour where a character will engage all active targets.
    /// </summary>
    public class EngageTargetsBehaviour : AbstractAlertBehaviourProvider
    {
        public override IBehaviour GetAlertBehaviour()
        {
            return BehaviourFactory.CreateEngageActiveTargetsBehaviour();
        }

        public override void Verify()
        {
        }
    }
}