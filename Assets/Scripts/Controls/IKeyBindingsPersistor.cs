namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Abstracts saving and Loading the KeyBindings.
    /// </summary>
    public interface IKeyBindingsPersistor
    {
        /// <summary>
        /// Saves the KeyBindings.
        /// </summary>
        /// <param name="keyBindings">The Keybindings to save.</param>
        void SaveKeyBindings(IKeyBindings keyBindings);

        /// <summary>
        /// Loads the KeyBindings.
        /// </summary>
        /// <returns>Returns the Loaded <see cref="IKeyBindings"/>.</returns>
        IKeyBindings LoadKeyBindings();
    }
}
