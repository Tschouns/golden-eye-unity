using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controlls the main menu screen.
    /// </summary>
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button creditsButton;

        [SerializeField]
        private Button quitButton;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action PlayButtonClicked;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action SettingsButtonClicked;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action CreditsButtonClicked;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action QuitButtonClicked;

        private void Awake()
        {
            Debug.Assert(this.playButton != null, "Play Button is not set!");
            Debug.Assert(this.settingsButton != null, "Settings Button is not set!");
            Debug.Assert(this.creditsButton != null, "Credits Button is not set!");
            Debug.Assert(this.quitButton != null, "Quit Button is not set!");

            this.playButton.onClick.AddListener(this.OnPlayButtonClicked);
            this.settingsButton.onClick.AddListener(this.OnSettingsButtonClicked);
            this.creditsButton.onClick.AddListener(this.OnCreditsButtonClicked);
            this.quitButton.onClick.AddListener(this.OnQuitButtonClicked);
        }

        private void OnDestroy()
        {
            this.playButton.onClick.RemoveListener(this.OnPlayButtonClicked);
            this.settingsButton.onClick.RemoveListener(this.OnSettingsButtonClicked);
            this.creditsButton.onClick.RemoveListener(this.OnCreditsButtonClicked);
            this.quitButton.onClick.RemoveListener(this.OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            PlayButtonClicked?.Invoke();
        }

        private void OnSettingsButtonClicked()
        {
            SettingsButtonClicked?.Invoke();
        }

        private void OnCreditsButtonClicked()
        {
            CreditsButtonClicked?.Invoke();
        }

        private void OnQuitButtonClicked()
        {
            QuitButtonClicked?.Invoke();
        }
    }
}
