
namespace Assets.Scripts.Missions
{
    /// <summary>
    /// Represents a specific mission objective. Each objective can be completed or failed individually.
    /// </summary>
    public interface IMissionObjective
    {
        /// <summary>
        /// Gets the mission objective description text.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the current mission goal status.
        /// </summary>
        MissionStatus Status { get; }
    }
}
