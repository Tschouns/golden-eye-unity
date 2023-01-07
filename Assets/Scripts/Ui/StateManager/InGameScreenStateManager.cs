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
        private static readonly float DEATH_ANIMATION_DURATION_SECONDS = 3.0f;

        private readonly GameOverScreen gameOverScreen;

        [SerializeField]
        private GameObject settingsMenu;

        [SerializeField]
        private GameObject gameOverAnimation;

        [SerializeField]
        private GameObject gameOverMenu;

        [SerializeField]
        private GameObject Hud;

        [SerializeField]
        private Health playerHealth;

        public bool isPaused { get; private set; } = false;

        public float MouseSensitivity { get; set; } = 1.0f;

        private void Awake()
        {
            // HUD
            Debug.Assert(this.Hud != null, "HUD is not set.");
            // Settings
            Debug.Assert(this.settingsMenu != null, "Settings menu is not set.");
            var settingsMenuController = this.settingsMenu.GetComponent<SettingsScreen>();
            settingsMenuController.Exited += this.ResumeGame;
            // GameOver Animation
            Debug.Assert(this.gameOverAnimation != null, "Game over animation is not set.");
            var deathAnimationScreen = this.gameOverAnimation.GetComponent<DeathAnimationScreen>();
            deathAnimationScreen.AnimationDurationSeconds = DEATH_ANIMATION_DURATION_SECONDS;
            // GameOver Menu
            Debug.Assert(this.gameOverMenu != null, "Game over menu is not set.");
            // Player Death
            this.playerHealth.Died += this.GameOver;
        }

        private void Update()
        {
            if (ControlsProvider.Actions.TogglePause)
            {
                if (this.isPaused)
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
            this.isPaused = true;
            Time.timeScale = 0;
            this.FocusUi();
            this.Hud.SetActive(false);
            this.settingsMenu.SetActive(true);
        }

        private void ResumeGame()
        {
            this.isPaused = false;
            this.FocusGame();
            this.settingsMenu.SetActive(false);
            this.Hud.SetActive(true);
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
            this.Hud.SetActive(false);
            // Activate the game over animation.
            this.gameOverAnimation.SetActive(true);
            // Wait for the animation to finish.
            yield return new WaitForSeconds(DEATH_ANIMATION_DURATION_SECONDS);
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
