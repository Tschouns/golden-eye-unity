using Assets.Scripts.Characters;
using Assets.Scripts.Gunplay.Inventory;
using Assets.Scripts.Missions;
using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    /// <summary>
    /// Updates the HUD.
    /// </summary>
    public class Hud : MonoBehaviour
    {
        private MissionController missionController;

        [SerializeField]
        private TextMeshProUGUI currentGunBulletCountText;

        [SerializeField]
        private TextMeshProUGUI inventoryBulletCountText;

        [SerializeField]
        private TextMeshProUGUI missionObjectivesText;

        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private BulletInventory bulletInventory;

        private void Awake()
        {
            Debug.Assert(this.currentGunBulletCountText != null);
            Debug.Assert(this.inventoryBulletCountText != null);
            Debug.Assert(this.missionObjectivesText != null);

            Debug.Assert(this.gunHandler != null);
            Debug.Assert(this.bulletInventory != null);

            this.missionController = FindObjectOfType<MissionController>();
        }

        private void Update()
        {
            this.UpdateBulletCountDisplay();
            this.UpdateMissionObjectivesDisplay();
        }

        private void UpdateBulletCountDisplay()
        {
            if (this.gunHandler.ActiveGun == null)
            {
                this.currentGunBulletCountText.text = "-";
                this.inventoryBulletCountText.text = "-";

                return;
            }

            this.currentGunBulletCountText.text = this.gunHandler.ActiveGun.CurrentNumberOfBullets.ToString();

            var inventoryBulletCount = this.bulletInventory.GetNumberOfBulletsForType(this.gunHandler.ActiveGun.Properties.Cartridge);
            this.inventoryBulletCountText.text = inventoryBulletCount.ToString();
        }

        private void UpdateMissionObjectivesDisplay()
        {
            if (this.missionController == null)
            {
                this.missionObjectivesText.text = "No mission.";
                return;
            }

            var text = new StringBuilder($"Mission Objectives ({this.GetStatusText(this.missionController.Status)}):\n");

            foreach (var objective in this.missionController.Objectives)
            {
                var optionalPrefix = objective.IsOptional ? "Optional: " : "";
                text.AppendLine($"- {optionalPrefix}{objective.Description}: {this.GetStatusText(objective.Status)}");
            }

            this.missionObjectivesText.text = text.ToString();
        }

        private string GetStatusText(MissionStatus status)
        {
            switch (status)
            {
                case MissionStatus.InProgress:
                    return "in progress";
                case MissionStatus.Completed:
                    return "completed";
                case MissionStatus.Failed:
                    return "failed";
                default:
                    throw new ArgumentException($"The specified mission status {status} is not supported.");
            }
        }
    }
}