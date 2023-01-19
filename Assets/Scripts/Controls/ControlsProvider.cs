using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Provides static access to control bindings.
    /// </summary>
    public static class ControlsProvider
    {
        private static readonly PlayerPrefsKeyBindingsPersistor persistor = new PlayerPrefsKeyBindingsPersistor();
        private static readonly string mouseSensitivityPrefKey = "MouseSensitivity";

        /// <summary>
        /// Initializes the <see cref="ControlsProvider"/> class.
        /// </summary>
        static ControlsProvider()
        {
            var keyBindings = persistor.LoadKeyBindings();
            Actions = new PlayerActions(keyBindings);

            if (PlayerPrefs.HasKey(mouseSensitivityPrefKey))
            {
                MouseSensitivity = PlayerPrefs.GetFloat(mouseSensitivityPrefKey);
            }
        }

        /// <summary>
        /// Gets the player actions.
        /// </summary>
        public static IPlayerActions Actions { get; private set; }

        /// <summary>
        /// Gets the mouse sensitivity.
        /// </summary>
        public static float MouseSensitivity { get; private set; } = 1f;

        /// <summary>
        /// Sets and persists specified key bindings.
        /// </summary>
        /// <param name="keyBindings">The key bindings.</param>
        public static void SetKeyBindings(IKeyBindings keyBindings)
        {
            persistor.SaveKeyBindings(keyBindings);
            Actions = new PlayerActions(keyBindings);
        }

        /// <summary>
        /// Sets and persists the specified mouse sensitivity.
        /// </summary>
        /// <param name="mouseSensitivity">
        /// The mouse sensitivity
        /// </param>
        public static void SetMouseSensitivity(float mouseSensitivity)
        {
            MouseSensitivity = mouseSensitivity;
            PlayerPrefs.SetFloat(mouseSensitivityPrefKey, mouseSensitivity);
        }

        /// <summary>
        /// Gets the current key bindings.
        /// </summary>
        public static IKeyBindings GetCurrentKeyBindings()
        {
            return persistor.LoadKeyBindings();
        }
    }
}
