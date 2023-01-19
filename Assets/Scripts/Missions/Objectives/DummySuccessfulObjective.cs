using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    /// <summary>
    /// A dummy objective (for testing) which is always successful.
    /// </summary>
    public class DummySuccessfulObjective : MonoBehaviour, IMissionObjective
    {
        [SerializeField]
        private string description = "Dummy objective.";

        [SerializeField]
        private bool isOptional = false;

        public string Description => this.description;
        public bool IsOptional => this.isOptional;
        public MissionStatus Status { get; private set; } = MissionStatus.Completed;
    }
}
