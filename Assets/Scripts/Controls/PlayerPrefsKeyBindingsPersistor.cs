using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class PlayerPrefsKeyBindingsPersistor : IKeyBindingsPersistor
    {
        /// <summary>
        /// Loads the KeyBindings from PlayerPrefs.
        /// </summary>
        /// <returns>The loaded <see cref="CustomKeyBindings"/></returns>
        public IKeyBindings LoadKeyBindings()
        {
            DefaultKeyBindings defaultKeyBindings = new();
            return new CustomKeyBindings
            {
                Forward = (KeyCode) PlayerPrefs.GetInt("Forward", (int) defaultKeyBindings.Forward),
                Backward = (KeyCode)
                    PlayerPrefs.GetInt("Backward", (int) defaultKeyBindings.Backward),
                Left = (KeyCode) PlayerPrefs.GetInt("Left", (int) defaultKeyBindings.Left),
                Right = (KeyCode) PlayerPrefs.GetInt("Right", (int) defaultKeyBindings.Right),
                Run = (KeyCode) PlayerPrefs.GetInt("Run", (int) defaultKeyBindings.Run),
                ToggleCrouch = (KeyCode)
                    PlayerPrefs.GetInt("ToggleCrouch", (int) defaultKeyBindings.ToggleCrouch),
                Reload = (KeyCode) PlayerPrefs.GetInt("Reload", (int) defaultKeyBindings.Reload),
                CycleWeapon = (KeyCode)
                    PlayerPrefs.GetInt("CycleWeapon", (int) defaultKeyBindings.CycleWeapon),
                TogglePause = (KeyCode)
                    PlayerPrefs.GetInt("TogglePause", (int) defaultKeyBindings.TogglePause)
            };
        }

        /// <summary>
        /// Saves the KeyBindings to the PlayerPrefs.
        /// </summary>
        /// <param name="keyBindings">The <see cref="IKeyBindings"/> to save to PlayerPrefs.</param>
        public void SaveKeyBindings(IKeyBindings keyBindings)
        {
            PlayerPrefs.SetInt("Forward", (int) keyBindings.Forward);
            PlayerPrefs.SetInt("Backward", (int) keyBindings.Backward);
            PlayerPrefs.SetInt("Left", (int) keyBindings.Left);
            PlayerPrefs.SetInt("Right", (int) keyBindings.Right);
            PlayerPrefs.SetInt("Run", (int) keyBindings.Run);
            PlayerPrefs.SetInt("ToggleCrouch", (int) keyBindings.ToggleCrouch);
            PlayerPrefs.SetInt("Reload", (int) keyBindings.Reload);
            PlayerPrefs.SetInt("CycleWeapon", (int) keyBindings.CycleWeapon);
            PlayerPrefs.SetInt("TogglePause", (int) keyBindings.TogglePause);
        }
    }
}
