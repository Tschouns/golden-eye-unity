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

        public bool Forward => Input.GetKey(keyBindings.Forward);
        public bool Backward => Input.GetKey(keyBindings.Backward);
        public bool Left => Input.GetKey(keyBindings.Left);
        public bool Right => Input.GetKey(keyBindings.Right);
        public bool Run => Input.GetKey(keyBindings.Run);
        public bool ToggleCrouch => Input.GetKeyDown(keyBindings.ToggleCrouch);
        public bool Trigger => Input.GetMouseButton(0);
        public bool Reload => Input.GetKeyDown(keyBindings.Reload);
        public bool CycleWeapon => Input.GetKeyDown(keyBindings.CycleWeapon);
        public bool TogglePause => Input.GetKeyDown(keyBindings.TogglePause);
    }
}
