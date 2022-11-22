﻿
using System.Collections.Generic;

namespace Assets.Scripts.Ai.Patrols
{
    /// <summary>
    /// Represents a patrol path.
    /// </summary>
    public interface IPatrolPath
    {
        /// <summary>
        /// Gets the patrol points.
        /// </summary>
        IReadOnlyList<IPatrolPoint> PatrolPoints { get; }
    }
}