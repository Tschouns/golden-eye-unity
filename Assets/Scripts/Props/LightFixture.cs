using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Gunplay.Effects;
using UnityEngine;

namespace Assets.Scripts.Props
{
    /// <summary>
    /// A light fixture prop which can be switched on and off.
    /// </summary>
    public class LightFixture : MonoBehaviour, IOnOff, IHitEffect
    {
        private bool isSwitchedOn = true;
        private bool isWorking = true;

        [SerializeField]
        private Renderer onModelRenderer;

        [SerializeField]
        private Renderer offModelRenderer;

        [SerializeField]
        private Light lightSource;

        public bool IsOn
        {
            get => this.isSwitchedOn && this.isWorking;
            set
            {
                this.isSwitchedOn = value;
                this.UpdateComponents();
            }
        }

        public void ReactToImpact(BulletImpact impact)
        {
            this.isWorking = false;
            this.UpdateComponents();
        }

        private void Awake()
        {
            Debug.Assert(this.onModelRenderer != null);
            Debug.Assert(this.offModelRenderer != null);
            Debug.Assert(this.lightSource != null);
        }

        private void UpdateComponents()
        {
            this.onModelRenderer.enabled = this.IsOn;
            this.offModelRenderer.enabled = !this.IsOn;
            this.lightSource.enabled = this.IsOn;
        }
    }
}
