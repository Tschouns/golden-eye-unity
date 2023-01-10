
using Assets.Scripts.Ai.Behaviour;

namespace Assets.Scripts.Ai.Configuration
{
    /// <summary>
    /// Provides a specific behaviour for when a character is "peaceful", i.e. unaware of any disturbence or enemy presence.
    /// </summary>
    public interface IPeacefulBehaviourProvider
    {
        /// <summary>
        /// Gets the specific "peaceful" behaviour.
        /// </summary>
        /// <returns>
        /// The specific "peaceful" behaviour
        /// </returns>
        IBehaviour GetPeacefulBehaviour();
    }
}