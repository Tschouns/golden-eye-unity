using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Specifies default key bindings.
    /// </summary>
    public class DefaultKeyBindings : IKeyBindings
    {
        public KeyCode Forward => KeyCode.W;
        public KeyCode Backward => KeyCode.S;
        public KeyCode Left => KeyCode.A;
        public KeyCode Right => KeyCode.D;
        public KeyCode Run => KeyCode.LeftShift;
        public KeyCode ToggleCrouch => KeyCode.LeftControl;
        public KeyCode Reload => KeyCode.R;
        public KeyCode CycleWeapon => KeyCode.C;
        public KeyCode TogglePause => KeyCode.P;
    }
}
