using UnityEngine;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Represents a patrol point.
    /// </summary>
    public interface IPatrolPoint
    {
        Vector3 Position { get; }
    }
}