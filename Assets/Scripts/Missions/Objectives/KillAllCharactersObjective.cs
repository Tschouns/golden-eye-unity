using Assets.Scripts.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    /// <summary>
    /// An objective to kill all characters of a specific faction and type.
    /// </summary>
    public class KillAllCharactersObjective : MonoBehaviour, IMissionObjective
    {
        private IReadOnlyList<ICharacter> targets;

        [SerializeField]
        private CharacterManager characterManager;

        [SerializeField]
        private string description = "Kill all enemies.";

        [SerializeField]
        private string faction = "soviet";

        [SerializeField]
        private string characterType = "combatant";

        public string Description { get; private set; }
        public MissionStatus Status { get; private set; } = MissionStatus.InProgress;

        private void Awake()
        {
            Debug.Assert(this.characterManager != null);
        }

        private void Update()
        {
            if (this.Status == MissionStatus.Completed)
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

            var nAlive = this.targets.Count(t => t.IsAlive);

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
