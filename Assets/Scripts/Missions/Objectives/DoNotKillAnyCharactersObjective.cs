using Assets.Scripts.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    /// <summary>
    /// An objective to NOT harm any characters of a specific faction and type.
    /// </summary>
    public class DoNotKillAnyCharactersObjective : MonoBehaviour, IMissionObjective
    {
        private IReadOnlyList<ICharacter> targets;

        [SerializeField]
        private CharacterManager characterManager;

        [SerializeField]
        private string description = "Do not harm characters.";

        [SerializeField]
        private bool isOptional = false;

        [SerializeField]
        private string faction = "soviet";

        [SerializeField]
        private string characterType = "civilian";

        public string Description => this.description;
        public bool IsOptional => this.isOptional;
        public MissionStatus Status { get; private set; } = MissionStatus.Completed;

        private void Awake()
        {
            Debug.Assert(this.characterManager != null);
        }

        private void Update()
        {
            if (this.Status == MissionStatus.Failed)
            {
                return;
            }

            // Initialize targets list.
            if (this.targets == null)
            {
                this.targets = this.characterManager.AllCharacters
                    .Where(
                        c => c.Faction == this.faction &&
                        c.CharacterType == this.characterType)
                    .ToList();
            }

            // Update status.
            if (this.targets.Any(t => !t.IsAlive))
            {
                this.Status = MissionStatus.Failed;
            }
        }
    }
}
