using UnityEngine.SceneManagement;

namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Controls the currently active scene.
    /// </summary>
    public static class GameSceneManager
    {
        /// <summary>
        /// Reloads the currently active scene.
        /// </summary>
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void ExitGame()
        {
            // This is a workaround for the fact that Application.Quit() does not work in WEBGL.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        /// <summary>
        /// Loads the Main Menu scene.
        /// </summary>
        public static void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Loads the Game scene.
        /// </summary>
        public static void LoadMap()
        {
            // TODO this should probably be refactored to something more stable.
            SceneManager.LoadScene(1);
        }
    }
}
