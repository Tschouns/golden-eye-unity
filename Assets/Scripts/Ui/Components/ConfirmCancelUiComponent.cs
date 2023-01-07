using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Components
{
    /// <summary>
    /// Controlls an overlay with two buttons for confirmation or cancelation.
    /// </summary>
    public class ConfirmCancelUiComponent : MonoBehaviour
    {
        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button cancelButton;

        public event Action Confirmed;
        public event Action Canceled;

        private void Awake()
        {
            Debug.Assert(this.confirmButton != null, "Confirm button is not set!");
            Debug.Assert(this.cancelButton != null, "Cancel button is not set!");
            this.confirmButton.onClick.AddListener(this.RaiseConfirmed);
            this.cancelButton.onClick.AddListener(this.RaiseCanceled);
        }

        private void OnDestroy()
        {
            this.confirmButton.onClick.RemoveListener(this.RaiseConfirmed);
            this.cancelButton.onClick.RemoveListener(this.RaiseCanceled);
        }

        private void RaiseConfirmed()
        {
            Confirmed?.Invoke();
        }

        private void RaiseCanceled()
        {
            Canceled?.Invoke();
        }
    }
}
