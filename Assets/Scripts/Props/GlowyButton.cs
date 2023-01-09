using UnityEngine;

namespace Assets.Scripts.Props
{
    /// <summary>
    /// A glowy button prop which can be switched on and off.
    /// </summary>
    public class GlowyButton : MonoBehaviour, IOnOff
    {
        [SerializeField]
        private Renderer onModelRenderer;

        [SerializeField]
        private Renderer offModelRenderer;

        public bool IsOn
        {
            get => this.onModelRenderer.enabled;
            set
            {
                this.onModelRenderer.enabled = value;
                this.offModelRenderer.enabled = !value;
            }
        }
    }
}