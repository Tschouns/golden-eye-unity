using Assets.Scripts.Controls;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Components
{
    /// <summary>
    /// Controlls the key rebinding component as part of the settings menu.
    /// </summary>
    public class KeyRebindingUiComponent : MonoBehaviour
    {
        private bool isListening = false;
        private Button listeningInputButton;
        private int[] keyValues;
        private readonly Dictionary<Button, TMP_Text> btnText = new();

        [SerializeField]
        private Button forward;

        [SerializeField]
        private Button backward;

        [SerializeField]
        private Button left;

        [SerializeField]
        private Button right;

        [SerializeField]
        private Button run;

        [SerializeField]
        private Button toggleCrouch;

        [SerializeField]
        private Button reload;

        [SerializeField]
        private Button cycleWeapon;

        public event Action<bool> KeyChangeRequested;

        private void Awake()
        {
            Debug.Assert(this.forward != null, "Input field for forward undefined!");
            Debug.Assert(this.backward != null, "Input field for backward undefined!");
            Debug.Assert(this.left != null, "Input field for left undefined!");
            Debug.Assert(this.right != null, "Input field for right undefined!");
            Debug.Assert(this.run != null, "Input field for run undefined!");
            Debug.Assert(this.toggleCrouch != null, "Input field for toggle crouch undefined!");
            Debug.Assert(this.reload != null, "Input field for reload undefined!");
            Debug.Assert(this.cycleWeapon != null, "Input field for cycle weapon undefined!");

            this.keyValues = (int[])System.Enum.GetValues(typeof(KeyCode));
            this.btnText.Add(this.forward, this.forward.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.backward, this.backward.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.left, this.left.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.right, this.right.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.run, this.run.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.toggleCrouch, this.toggleCrouch.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.reload, this.reload.GetComponentInChildren<TMP_Text>());
            this.btnText.Add(this.cycleWeapon, this.cycleWeapon.GetComponentInChildren<TMP_Text>());
        }

        private void Start()
        {
            // Execute in Start to make sure the keybindings are loaded.
            this.FillCurrentLayout();
            // Add listeners to buttons.
            this.forward.onClick.AddListener(() => this.ListenForInput(this.forward));
            this.backward.onClick.AddListener(() => this.ListenForInput(this.backward));
            this.left.onClick.AddListener(() => this.ListenForInput(this.left));
            this.right.onClick.AddListener(() => this.ListenForInput(this.right));
            this.run.onClick.AddListener(() => this.ListenForInput(this.run));
            this.toggleCrouch.onClick.AddListener(() => this.ListenForInput(this.toggleCrouch));
            this.reload.onClick.AddListener(() => this.ListenForInput(this.reload));
            this.cycleWeapon.onClick.AddListener(() => this.ListenForInput(this.cycleWeapon));
        }

        private void OnEnable()
        {
            // Reload on each opening of the menu.
            this.FillCurrentLayout();
        }

        private void Update()
        {
            // this could be done easier with the new input system.
            // Solution was found on forum: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
            if (this.isListening)
            {
                for (int i = 0; i < this.keyValues.Length; i++)
                {
                    var key = (KeyCode)this.keyValues[i];

                    if (Input.GetKey(key))
                    {
                        this.CancelListening();
                        this.SetButtonText(this.listeningInputButton, key);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the Keybindings that the user defined himself.
        /// </summary>
        /// <returns>The <see cref="IKeyBindings"/> that the user specified.</returns>
        public IKeyBindings GetKeyBindings()
        {
            return new CustomKeyBindings
            {
                Forward = this.GetKeyFromBtn(this.forward),
                Backward = this.GetKeyFromBtn(this.backward),
                Left = this.GetKeyFromBtn(this.left),
                Right = this.GetKeyFromBtn(this.right),
                Run = this.GetKeyFromBtn(this.run),
                ToggleCrouch = this.GetKeyFromBtn(this.toggleCrouch),
                Reload = this.GetKeyFromBtn(this.reload),
                CycleWeapon = this.GetKeyFromBtn(this.cycleWeapon),
                // TODO: Add toggle pause key binding
                TogglePause = new DefaultKeyBindings().TogglePause
            };
        }

        /// <summary>
        /// The Rebinding process is finished.
        /// </summary>
        private void CancelListening()
        {
            KeyChangeRequested?.Invoke(false);
            this.isListening = false;
        }

        private void FillCurrentLayout()
        {
            var keyBindings = ControlsProvider.GetCurrentKeyBindings();
            Debug.Log(keyBindings.ToString());
            this.SetButtonText(this.forward, keyBindings.Forward);
            this.SetButtonText(this.backward, keyBindings.Backward);
            this.SetButtonText(this.left, keyBindings.Left);
            this.SetButtonText(this.right, keyBindings.Right);
            this.SetButtonText(this.run, keyBindings.Run);
            this.SetButtonText(this.toggleCrouch, keyBindings.ToggleCrouch);
            this.SetButtonText(this.reload, keyBindings.Reload);
            this.SetButtonText(this.cycleWeapon, keyBindings.CycleWeapon);
        }

        /// <summary>
        /// Activates the listening mode for the given button.
        /// </summary>
        /// <param name="keyCodeButton"></param>
        private void ListenForInput(Button keyCodeButton)
        {
            this.isListening = true;
            KeyChangeRequested?.Invoke(true);
            this.listeningInputButton = keyCodeButton;
        }

        /// <summary>
        /// Converts the text of the button to a KeyCode.
        /// </summary>
        /// <param name="btn">The <see cref="Button"/>.</param>
        /// <returns><see cref="KeyCode"/> which corresponds to the text in the button.</returns>
        private KeyCode GetKeyFromBtn(Button btn)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), this.btnText[btn].text);
        }

        private void SetButtonText(Button button, KeyCode keyCode)
        {
            this.btnText[button].text = keyCode.ToString();
        }
    }
}
