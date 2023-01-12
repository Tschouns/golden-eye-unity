using System.Collections.Generic;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Provides all the available escape points.
    /// </summary>
    public interface IEscapePointProvider
    {
        /// <summary>
        /// Gets all the available escape points.
        /// </summary>
        IEnumerable<IEscapePoint> EscapePoints { get; }
    }
}