using Assets.Scripts.Misc;
using Assets.Scripts.Ui.Screen;
using UnityEngine;

namespace Assets.Scripts.Ui.StateManager
{
    /// <summary>
    /// Manages the state, visibility and behaviour of the different screens in the main menu.
    /// </summary>
    public class MainMenuScreenStateManager : MonoBehaviour
    {
        private MainMenuScreen mainMenuScreen;
        private SettingsScreen settingsMenuScreen;
        private CreditsScreen creditsScreen;

        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private GameObject settingsMenu;

        [SerializeField]
        private GameObject creditsUi;

        private void Awake()
        {
            this.mainMenuScreen = this.mainMenu.GetComponent<MainMenuScreen>();
            this.settingsMenuScreen = this.settingsMenu.GetComponent<SettingsScreen>();
            this.creditsScreen = this.creditsUi.GetComponent<CreditsScreen>();

            Debug.Assert(this.mainMenuScreen != null, "Main Menu Controller is not set!");
            Debug.Assert(this.settingsMenuScreen != null, "Settings Menu Controller is not set!");
            Debug.Assert(this.creditsScreen != null, "Credits Ui Controller is not set!");

            Debug.Assert(this.mainMenu != null, "Main Menu is not set!");
            Debug.Assert(this.settingsMenu != null, "Settings Menu is not set!");
            Debug.Assert(this.creditsUi != null, "Credits Ui is not set!");

            this.mainMenuScreen.PlayButtonClicked += this.StartGame;
            this.mainMenuScreen.SettingsButtonClicked += this.ShowSettingsMenu;
            this.mainMenuScreen.CreditsButtonClicked += this.ShowCreditsUi;
            this.mainMenuScreen.QuitButtonClicked += GameSceneManager.ExitGame;
            this.settingsMenuScreen.Exited += this.ShowMainMenu;
            this.creditsScreen.BackButtonClicked += this.ShowMainMenu;
        }

        private void StartGame()
        {
            GameSceneManager.LoadMap();
        }

        private void ShowSettingsMenu()
        {
            this.mainMenu.SetActive(false);
            this.settingsMenu.SetActive(true);
        }

        private void ShowCreditsUi()
        {
            this.mainMenu.SetActive(false);
            this.creditsUi.SetActive(true);
        }

        private void ShowMainMenu()
        {
            this.settingsMenu.SetActive(false);
            this.creditsUi.SetActive(false);
            this.mainMenu.SetActive(true);
        }
    }
}
