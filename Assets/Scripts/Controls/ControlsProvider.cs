
namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Provides static access to control bindings.
    /// </summary>
    public static class ControlsProvider
    {
        /// <summary>
        /// Gets the player actions.
        /// </summary>
        public static IPlayerActions Actions { get; } = new PlayerActions(new DefaultKeyBindings());
    }
}
