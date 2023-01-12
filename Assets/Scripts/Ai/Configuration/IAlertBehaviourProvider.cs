using Assets.Scripts.Ai.Behaviour;

namespace Assets.Scripts.Ai.Configuration
{
    /// <summary>
    /// Provides a specific behaviour for when a character is "alert" to enemy presence.
    /// </summary>
    public interface IAlertBehaviourProvider
    {
        /// <summary>
        /// Gets the specific "alert" behaviour.
        /// </summary>
        /// <returns>
        /// The specific "alert" behaviour
        /// </returns>
        IBehaviour GetAlertBehaviour();
    }
}