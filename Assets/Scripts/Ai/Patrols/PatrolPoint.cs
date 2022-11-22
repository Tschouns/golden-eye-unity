using UnityEngine;

namespace Assets.Scripts.Ai.Patrols
{
    /// <summary>
    /// Designates an object a patrol point.
    /// </summary>
    public class PatrolPoint : MonoBehaviour, IPatrolPoint
    {
        public Vector3 Position => this.transform.position;
    }
}
