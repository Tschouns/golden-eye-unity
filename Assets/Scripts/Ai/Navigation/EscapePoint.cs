using UnityEngine;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Designates an object an escape point.
    /// </summary>
    public class EscapePoint : MonoBehaviour, IEscapePoint
    {
        public Vector3 Position => this.transform.position;
    }
}
