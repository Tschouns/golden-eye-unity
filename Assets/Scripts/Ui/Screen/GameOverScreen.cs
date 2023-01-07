using Assets.Scripts.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controlls the GameOver screen.
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button exitButton;

        private void Awake()
        {
            Debug.Assert(this.restartButton != null, "Restart button is not set!");
            this.restartButton.onClick.AddListener(this.OnRestartClicked);
            Debug.Assert(this.exitButton != null, "Exit button is not set!");
            this.exitButton.onClick.AddListener(this.OnExitClicked);
        }

        private void OnDestroy()
        {
            this.restartButton.onClick.RemoveListener(this.OnRestartClicked);
            this.exitButton.onClick.RemoveListener(this.OnExitClicked);
        }

        private void OnRestartClicked()
        {
            GameSceneManager.ReloadScene();
        }

        private void OnExitClicked()
        {
            GameSceneManager.ExitGame();
        }
    }
}
