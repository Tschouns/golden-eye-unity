using Assets.Scripts.Damage;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Components
{
    /// <summary>
    /// Controlls the simple health slider.
    /// </summary>
    public class HealthBarUiComponent : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Health health;

        private void Awake()
        {
            Debug.Assert(this.slider != null, $"{nameof(this.slider)} is not set!");
            Debug.Assert(this.health != null, $"{nameof(this.health)} is not set!");
        }

        private void Start()
        {
            this.slider.value = this.health.CurrentHealth;
            this.health.HealthChanged += this.OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            var percentage = this.health.CurrentHealth / (float)this.health.MaxHealth;
            this.slider.value = Math.Max(0f, percentage);
        }
    }
}
