using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class PlayerActions : IPlayerActions
    {
        private readonly IKeyBindings keyBindings;

        public PlayerActions(IKeyBindings keyBindings)
        {
            Debug.Assert(keyBindings != null);

            this.keyBindings = keyBindings;
        }

        public bool Forward => Input.GetKey(this.keyBindings.Forward);
        public bool Backward => Input.GetKey(this.keyBindings.Backward);
        public bool Left => Input.GetKey(this.keyBindings.Left);
        public bool Right => Input.GetKey(this.keyBindings.Right);
        public bool Run => Input.GetKey(this.keyBindings.Run);
        public bool ToggleCrouch => Input.GetKeyDown(this.keyBindings.ToggleCrouch);
        public bool Reload => Input.GetKeyDown(this.keyBindings.Reload);
        public bool CycleWeapon => Input.GetKeyDown(this.keyBindings.CycleWeapon);
    }
}
