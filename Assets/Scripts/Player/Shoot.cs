using Assets.Scripts.Controls;
using Assets.Scripts.Gunplay.Guns;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Allows a character to have and manipulate a gun.
    /// </summary>
    public class Shoot : PlayerInputBase
    {
        [SerializeField]
        private Gun activeGun;

        private void Update()
        {
            if (this.activeGun != null &&
                ControlsProvider.Actions.Trigger)
            {
                this.activeGun.Trigger();
            }
        }
    }
}
