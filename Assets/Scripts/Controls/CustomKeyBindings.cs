using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Allows the player to customize their key bindings in Runtime.
    /// </summary>
    public class CustomKeyBindings : IKeyBindings
    {
        public KeyCode Forward { get; set; }

        public KeyCode Backward { get; set; }

        public KeyCode Left { get; set; }

        public KeyCode Right { get; set; }

        public KeyCode Run { get; set; }

        public KeyCode ToggleCrouch { get; set; }

        public KeyCode Reload { get; set; }

        public KeyCode CycleWeapon { get; set; }

        public KeyCode TogglePause { get; set; }
    }
}
