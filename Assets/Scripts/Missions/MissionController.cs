using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Missions
{
    /// <summary>
    /// Tracks the status of THE mission and its mission objectives. There can only be ONE mission per scene.
    /// </summary>
    public class MissionController : MonoBehaviour, IMission
    {
        private readonly IList<IMissionObjective> missionObjectiveList = new List<IMissionObjective>();

        public event Action Completed;
        public event Action Failed;

        public MissionStatus Status { get; private set; } = MissionStatus.InProgress;

        public IEnumerable<IMissionObjective> Objectives => this.missionObjectiveList;

        private void Awake()
        {
            // Retrieve mission objectives.
            var goals = this.GetComponentsInChildren<IMissionObjective>();
            this.missionObjectiveList.AddRange(goals);

            Debug.Assert(goals.Any(), "There are no mission objectives. Add mission objectives as children of the mission controller.");
        }

        private void Update()
        {
            if (this.Status is MissionStatus.Completed or
                MissionStatus.Failed)
            {
                return;
            }

            // If any objective fails, the mission fails.
            if (this.Objectives.Any(o => o.Status == MissionStatus.Failed))
            {
                this.Status = MissionStatus.Failed;
                Failed?.Invoke();

                return;
            }

            // Only if and when all the objectives have been successfully completed, the mission is successful.
            if (this.Objectives.All(o => o.Status == MissionStatus.Completed))
            {
                this.Status = MissionStatus.Completed;
                Completed?.Invoke();

                return;
            }

            this.Status = MissionStatus.InProgress;
        }
    }
}
