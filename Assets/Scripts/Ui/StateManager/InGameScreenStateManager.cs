using Assets.Scripts.Controls;
using Assets.Scripts.Damage;
using Assets.Scripts.Ui.Screen;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ui.StateManager
{
    /// <summary>
    /// Manages the state, visibility and transitions of the in-game screens.
    /// </summary>
    public class InGameScreenStateManager : MonoBehaviour
    {
        private static readonly float deathAnimationDurationSeconds = 3.0f;

        [SerializeField]
        private GameObject settingsMenu;

        [SerializeField]
        private GameObject gameOverAnimation;

        [SerializeField]
        private GameObject gameOverMenu;

        [SerializeField]
        private GameObject hud;

        [SerializeField]
        private Health playerHealth;

        /// <summary>
        /// Gets or sets a value indicating whether the game is currently paused.
        /// </summary>
        public bool IsPaused { get; private set; } = false;

        private void Awake()
        {
            Debug.Assert(this.settingsMenu != null, "Settings menu is not set.");
            Debug.Assert(this.gameOverAnimation != null, "Game over animation is not set.");
            Debug.Assert(this.gameOverMenu != null, "Game over menu is not set.");
            Debug.Assert(this.hud != null, "HUD is not set.");
            Debug.Assert(this.playerHealth != null, "PLayer Health is not set.");

            var settingsMenuController = this.settingsMenu.GetComponent<SettingsScreen>();
            var deathAnimationScreen = this.gameOverAnimation.GetComponent<DeathAnimationScreen>();

            Debug.Assert(settingsMenuController != null);
            Debug.Assert(deathAnimationScreen != null);

            settingsMenuController.Exited += this.ResumeGame;
            deathAnimationScreen.AnimationDurationSeconds = deathAnimationDurationSeconds;

            this.playerHealth.Died += this.GameOver;
        }

        private void Update()
        {
            if (ControlsProvider.Actions.TogglePause)
            {
                if (this.IsPaused)
                {
                    this.ResumeGame();
                }
                else
                {
                    this.PauseGame();
                }
            }
        }

        private void PauseGame()
        {
            this.IsPaused = true;
            Time.timeScale = 0;
            this.FocusUi();
            this.hud.SetActive(false);
            this.settingsMenu.SetActive(true);
        }

        private void ResumeGame()
        {
            this.IsPaused = false;
            this.FocusGame();
            this.settingsMenu.SetActive(false);
            this.hud.SetActive(true);
            Time.timeScale = 1;
        }

        /// <summary>
        /// Displays the death animation and the game over screen afterwards.
        /// </summary>
        private void GameOver()
        {
            _ = this.StartCoroutine(this.PlayGameOverAnimation());
        }

        private IEnumerator PlayGameOverAnimation()
        {
            this.hud.SetActive(false);
            // Activate the game over animation.
            this.gameOverAnimation.SetActive(true);
            // Wait for the animation to finish.
            yield return new WaitForSeconds(deathAnimationDurationSeconds);
            // After we have waited for the animation to finish, show the Game Over Screen.
            this.gameOverAnimation.SetActive(false);
            this.FocusUi();
            this.gameOverMenu.SetActive(true);
        }

        private void FocusGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FocusUi()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
