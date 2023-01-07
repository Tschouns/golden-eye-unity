namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Provides static access to control bindings.
    /// </summary>
    public static class ControlsProvider
    {
        /// <summary>
        /// Initializes the <see cref="ControlsProvider"/> class.
        /// </summary>
        static ControlsProvider()
        {
            Persistor = new PlayerPrefsKeyBindingsPersistor();
            var keyBindings = Persistor.LoadKeyBindings();
            Actions = new PlayerActions(keyBindings);
        }

        /// <summary>
        /// Gets the key bindings persistor.
        /// </summary>
        private static IKeyBindingsPersistor Persistor { get; set; }

        /// <summary>
        /// Gets the player actions.
        /// </summary>
        public static IPlayerActions Actions { get; private set; }

        /// <summary>
        /// Persists the changed state of the <see cref="IPlayerActions"/>.
        /// </summary>
        /// <param name="keyBindings">The key bindings.</param>
        public static void SaveAndReloadPlayerActions(IKeyBindings keyBindings)
        {
            Persistor.SaveKeyBindings(keyBindings);
            Actions = new PlayerActions(keyBindings);
        }

        /// <summary>
        /// Gets the current key bindings.
        /// </summary>
        public static IKeyBindings GetCurrentKeyBindings()
        {
            return Persistor.LoadKeyBindings();
        }
    }
}
