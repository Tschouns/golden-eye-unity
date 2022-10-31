using Assets.Scripts.Gunplay.Ballistics;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Represents a reaction to a bullet hit.
    /// </summary>
    public interface IHitEffect
    {
        /// <summary>
        /// Prompts a reaction to the specified bullet hit.
        /// </summary>
        /// <param name="impact">
        /// The bullet impact information
        /// </param>
        void ReactToImpact(BulletImpact impact);
    }
}
