using Assets.Scripts.Characters;
using Assets.Scripts.Gunplay.Inventory;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controlls the HUD.
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
            Debug.Assert(this.currentGunBulletCountText != null, "Current gun bullet count text is not set.");
            Debug.Assert(this.inventoryBulletCountText != null, "Inventory bullet count text is not set.");
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

            int inventoryBulletCount = this.bulletInventory.GetNumberOfBulletsForType(this.gunHandler.ActiveGun.Properties.Cartridge);
            this.inventoryBulletCountText.text = inventoryBulletCount.ToString();
        }
    }
}