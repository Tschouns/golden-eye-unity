using Assets.Scripts.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Components
{
    /// <summary>
    /// Controlls the gameplay settings menu component.
    /// </summary>
    public class GameplaySettingsUiComponent : MonoBehaviour
    {
        private static readonly string masterVolumeKey = "MasterVolume";

        private float previousMasterVolume = 0.5f;

        [SerializeField]
        private Slider masterVolumeSlider;

        [SerializeField]
        private Slider mouseSensitivitySlider;


        private void Awake()
        {
            Debug.Assert(this.masterVolumeSlider != null, "'Master volume slider' is not defined!");
            Debug.Assert(this.mouseSensitivitySlider != null, "'Mouse sensitivity slider' is not defined!");

            this.masterVolumeSlider.onValueChanged.AddListener(this.OnVolumeChanged);
        }

        private void OnEnable()
        {
            this.masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolumeKey, this.previousMasterVolume);
            this.previousMasterVolume = this.masterVolumeSlider.value;

            this.mouseSensitivitySlider.value = ControlsProvider.MouseSensitivity;
        }

        /// <summary>
        /// Saves the settings to the PlayerPrefs.
        /// </summary>
        public void SaveSettings()
        {
            PlayerPrefs.SetFloat(masterVolumeKey, this.masterVolumeSlider.value);
            this.previousMasterVolume = this.masterVolumeSlider.value;
            ControlsProvider.SetMouseSensitivity(this.mouseSensitivitySlider.value);
        }

        /// <summary>
        /// Reloads the Settings from the PlayerPrefs.
        /// </summary>
        public void ResetSettings()
        {
            this.masterVolumeSlider.value = this.previousMasterVolume;
            AudioListener.volume = this.previousMasterVolume;
            this.mouseSensitivitySlider.value = ControlsProvider.MouseSensitivity;
        }

        private void OnVolumeChanged(float vol)
        {
            AudioListener.volume = vol;
        }
    }
}
