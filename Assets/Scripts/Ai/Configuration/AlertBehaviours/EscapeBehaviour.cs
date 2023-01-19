using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;
using System.Linq;

namespace Assets.Scripts.Ai.Configuration.AlertBehaviours
{
    /// <summary>
    /// Provides an "alert" behaviour where a character will escape.
    /// </summary>
    public class EscapeBehaviour : AbstractAlertBehaviourProvider
    {
        public override IBehaviour GetAlertBehaviour()
        {
            return BehaviourFactory.CreateEscapeBehaviour();
        }

        public override void Verify()
        {
        }
    }
}