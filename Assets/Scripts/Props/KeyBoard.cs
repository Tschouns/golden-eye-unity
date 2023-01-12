using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Props
{
    /// <summary>
    /// A key board prop with a bunch of randomly blinking buttons.
    /// </summary>
    public class KeyBoard : MonoBehaviour, IOnOff
    {
        private IEnumerable<GlowyButton> glowyButtons;
        private bool isOn = true;

        [SerializeField]
        private float binkyInterval = 1.5f;

        public bool IsOn
        {
            get => this.isOn;
            set
            {
                this.isOn = value;
                if (value)
                {
                    this.StartCoroutine(this.ChangeStateRepeatedly());
                }
                else
                {
                    this.DeactivateButtons();
                }
            }
        }

        private void Awake()
        {
            this.glowyButtons = this.GetComponentsInChildren<GlowyButton>();
            Debug.Assert(this.glowyButtons != null && this.glowyButtons.Any(), "No glowy buttons found.");

            this.StartCoroutine(this.ChangeStateRepeatedly());
        }

        private IEnumerator ChangeStateRepeatedly()
        {
            while (this.IsOn)
            {
                this.SetRandomGlowyButtonState();
                yield return new WaitForSeconds(this.binkyInterval);
            }
        }

        private void SetRandomGlowyButtonState()
        {
            var random = new System.Random();
            foreach (var button in this.glowyButtons)
            {
                button.IsOn = this.IsOn && random.Next(0, 3) == 0;
            }
        }

        private void DeactivateButtons()
        {
            foreach (var button in this.glowyButtons)
            {
                button.IsOn = false;
            }
        }
    }
}
