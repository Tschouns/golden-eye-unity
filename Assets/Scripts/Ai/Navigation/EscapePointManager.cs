using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ai.Navigation
{
    /// <summary>
    /// Collects and provides all the available escape points in a scene. Should be unique within the scene.
    /// </summary>
    public class EscapePointManager : MonoBehaviour, IEscapePointProvider
    {
        public IEnumerable<IEscapePoint> EscapePoints { get; private set; } = new List<IEscapePoint>();

        private void Awake()
        {
            this.EscapePoints = FindObjectsOfType<EscapePoint>();
        }
    }
}