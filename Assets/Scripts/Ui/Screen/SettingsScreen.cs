using Assets.Scripts.Controls;
using Assets.Scripts.Ui.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controls the settings screen.
    /// </summary>
    public class SettingsScreen : MonoBehaviour
    {
        private bool wasKeyChangeRequested = false;
        private ConfirmCancelUiComponent confirmSavePrompt;
        private KeyRebindingUiComponent keysUiController;
        private GameplaySettingsUiComponent settingsUiController;

        [SerializeField]
        private Button saveButton;

        [SerializeField]
        private Button exitButton;

        [SerializeField]
        private GameObject overlayWaitingForInput;

        [SerializeField]
        private GameObject overlaySavingPrompt;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action Exited;

        private void Awake()
        {
            // Overlay
            Debug.Assert(this.overlayWaitingForInput != null, "'Settings overlay' is not set!");
            Debug.Assert(this.overlaySavingPrompt != null, "'Confirm overlay' is not set!");

            this.confirmSavePrompt = this.overlaySavingPrompt.GetComponent<ConfirmCancelUiComponent>();
            Debug.Assert(this.confirmSavePrompt != null, "'Confirm Ui controller' is not set!");

            this.confirmSavePrompt.Confirmed += this.OnSavePromptSaveClicked;
            this.confirmSavePrompt.Canceled += this.OnSavePromptExitClicked;

            // Controllers
            this.keysUiController = this.GetComponentInChildren<KeyRebindingUiComponent>();
            Debug.Assert(this.keysUiController != null, "'Key rebinding controller' is not set in children!");

            this.keysUiController.KeyChangeRequested += this.KeyChangeRequested;
            this.settingsUiController = this.GetComponentInChildren<GameplaySettingsUiComponent>();
            Debug.Assert(this.settingsUiController != null, "'Gameplay settings controller' is not set in children!");

            // Buttons
            Debug.Assert(this.saveButton != null, "Save button is not set!");
            Debug.Assert(this.exitButton != null, "Exit button is not set!");
            this.saveButton.onClick.AddListener(this.OnSaveClicked);
            this.exitButton.onClick.AddListener(this.OnExitClicked);
        }

        private void OnDestroy()
        {
            this.saveButton.onClick.RemoveListener(this.OnSaveClicked);
            this.exitButton.onClick.RemoveListener(this.OnExitClicked);
            this.confirmSavePrompt.Confirmed -= this.OnSavePromptSaveClicked;
            this.confirmSavePrompt.Canceled -= this.OnSavePromptExitClicked;
        }

        /// <summary>
        /// Saves the current state of the settings.
        /// </summary>
        private void SaveSettings()
        {
            // Gameplay settings
            this.settingsUiController.SaveSettings();

            // Key Bindings
            var keyBindings = this.keysUiController.GetKeyBindings();
            ControlsProvider.SaveAndReloadPlayerActions(keyBindings);
        }

        /// <summary>
        /// The listener for the save button.
        /// </summary>
        private void OnSaveClicked()
        {
            // save
            this.SaveSettings();
            // Overlay
            this.wasKeyChangeRequested = false;
        }

        /// <summary>
        /// The listener for the exit button.
        /// </summary>
        private void OnExitClicked()
        {
            if (this.wasKeyChangeRequested)
            {
                this.overlaySavingPrompt.SetActive(true);
                return;
            }
            // close
            Exited?.Invoke();
        }

        /// <summary>
        /// Hides and shows the overlay.
        /// If there was a key change requested, the exit button will show a confirmation overlay.
        /// </summary>
        /// <param name="isRequested">If there was a change requested or canceled.</param>
        private void KeyChangeRequested(bool isRequested)
        {
            this.overlayWaitingForInput.SetActive(isRequested);
            this.wasKeyChangeRequested = true;
        }

        /// <summary>
        /// If a key change was requested, and the user cancels the saving-prompt, this method will be called.
        /// </summary>
        private void OnSavePromptExitClicked()
        {
            this.wasKeyChangeRequested = false;
            // overlay
            this.overlaySavingPrompt.SetActive(false);
            // close
            Exited?.Invoke();
        }

        /// <summary>
        /// If a key change was requested, and the user confirms the saving-prompt, this method will be called.
        /// </summary>
        private void OnSavePromptSaveClicked()
        {
            // save
            this.SaveSettings();
            // overlay
            this.overlaySavingPrompt.SetActive(false);
            // close
            Exited?.Invoke();
        }
    }
}
