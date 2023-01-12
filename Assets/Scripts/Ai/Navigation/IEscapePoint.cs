using UnityEngine;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Represents an escape point, i.e. a point a character may escape to when under threat.
    /// </summary>
    public interface IEscapePoint
    {
        Vector3 Position { get; }
    }
}