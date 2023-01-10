using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;

namespace Assets.Scripts.Ai.Configuration.AlertBehaviours
{
    /// <summary>
    /// Provides an "alert" behaviour where a character will escape.
    /// </summary>
    public class EscapeBehaviour : AbstractAlertBehaviourProvider
    {
        public override IBehaviour GetAlertBehaviour()
        {
            return new EscapeToEscapePoint();
        }

        public override void Verify()
        {
        }
    }
}