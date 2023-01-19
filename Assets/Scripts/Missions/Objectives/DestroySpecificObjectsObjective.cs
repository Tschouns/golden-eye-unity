using Assets.Scripts.Damage;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    /// <summary>
    /// An objective to destroy specific target objects.
    /// </summary>
    public class DestroySpecificObjectsObjective : MonoBehaviour, IMissionObjective
    {
        [SerializeField]
        private string description = "Destroy objects.";

        [SerializeField]
        private bool isOptional = false;

        [SerializeField]
        private Health[] targetObjects;

        public string Description { get; private set; }
        public bool IsOptional => this.isOptional;
        public MissionStatus Status { get; private set; } = MissionStatus.InProgress;

        private void Awake()
        {
            Debug.Assert(this.targetObjects != null);
        }

        private void Update()
        {
            if (this.Status == MissionStatus.Completed)
            {
                return;
            }

            var nAlive = this.targetObjects.Count(t => t.IsAlive);

            // Update description.
            this.Description = $"{this.description} ({nAlive} remaining)";

            // Update status.
            if (nAlive == 0)
            {
                this.Status = MissionStatus.Completed;
            }
        }
    }
}
