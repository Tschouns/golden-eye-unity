using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Screen
{
    /// <summary>
    /// Controls the Credits Screen.
    /// </summary>
    public class CreditsScreen : MonoBehaviour
    {
        [SerializeField]
        private Button backButton;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public event Action BackButtonClicked;

        private void Awake()
        {
            Debug.Assert(this.backButton != null, "Back Button is not set!");
            this.backButton.onClick.AddListener(this.OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            this.backButton.onClick.RemoveListener(this.OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            BackButtonClicked?.Invoke();
        }
    }
}
