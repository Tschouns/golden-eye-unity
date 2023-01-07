using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Components
{
    /// <summary>
    /// Controlls the gameplay settings menu component.
    /// </summary>
    public class GameplaySettingsUiComponent : MonoBehaviour
    {
        private float previousMasterVolume = 0.5f;

        [SerializeField]
        private Slider masterVolumeSlider;

        private void Awake()
        {
            Debug.Assert(this.masterVolumeSlider != null, "'Master volume slider' is not defined!");
            this.masterVolumeSlider.onValueChanged.AddListener(this.OnVolumeChanged);
        }

        private void OnEnable()
        {
            this.masterVolumeSlider.value = PlayerPrefs.GetFloat(
                "MasterVolume",
                this.previousMasterVolume
            );
            this.previousMasterVolume = this.masterVolumeSlider.value;
        }

        /// <summary>
        /// Saves the settings to the PlayerPrefs.
        /// </summary>
        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("MasterVolume", this.masterVolumeSlider.value);
            this.previousMasterVolume = this.masterVolumeSlider.value;
        }

        /// <summary>
        /// Reloads the Settings from the PlayerPrefs.
        /// </summary>
        public void ResetSettings()
        {
            this.masterVolumeSlider.value = this.previousMasterVolume;
            AudioListener.volume = this.previousMasterVolume;
        }

        private void OnVolumeChanged(float vol)
        {
            AudioListener.volume = vol;
        }
    }
}
