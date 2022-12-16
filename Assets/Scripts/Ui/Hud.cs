using Assets.Scripts.Characters;
using Assets.Scripts.Gunplay.Inventory;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    /// <summary>
    /// Updates the HUD.
    /// </summary>
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI currentGunBulletCountText;

        [SerializeField]
        private TextMeshProUGUI inventoryBulletCountText;

        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private BulletInventory bulletInventory;

        private void Awake()
        {
            Debug.Assert(this.currentGunBulletCountText != null);
            Debug.Assert(this.inventoryBulletCountText != null);
        }

        private void Update()
        {
            this.UpdateBulletCountDisplay();
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
    }
}