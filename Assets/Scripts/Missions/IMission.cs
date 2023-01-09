
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Missions
{
    /// <summary>
    /// Represents a mission.
    /// </summary>
    public interface IMission
    {
        /// <summary>
        /// Is fired when the mission has been completed.
        /// </summary>
        event Action Completed;

        /// <summary>
        /// Is fired when the mission has failed.
        /// </summary>
        event Action Failed;

        /// <summary>
        /// Gets the mission's overall status.
        /// </summary>
        MissionStatus Status { get; }

        /// <summary>
        /// Gets the missions individual mission objectives.
        /// </summary>
        IEnumerable<IMissionObjective> Objectives { get; }
    }
}
