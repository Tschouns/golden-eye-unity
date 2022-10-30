
using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Provides the key binding for each player action.
    /// </summary>
    public interface IKeyBindings
    {
        KeyCode Forward { get; }
        KeyCode Backward { get; }
        KeyCode Left { get; }
        KeyCode Right { get; }
        KeyCode Run { get; }
        KeyCode ToggleCrouch { get; }
        KeyCode Reload { get; }
        KeyCode CycleWeapon { get; }
    }
}
