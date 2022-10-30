using System;
using UnityEngine;

namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Tracks a entity's health.
    /// </summary>
    public class Health : MonoBehaviour, IHealth, ITakeDamage
    {
        [SerializeField]
        private int maxHealth = 100;

        private bool hasDied = false;

        public event Action Died;

        public int MaxHealth => Math.Abs(this.maxHealth);

        public int CurrentHealth { get; private set; }

        public bool IsAlive => this.CurrentHealth > 0;

        public void Damage(int damage)
        {
            // Adjust health.
            damage = Math.Abs(damage);
            this.CurrentHealth = Math.Max(0, this.CurrentHealth - damage);

            Debug.Log($"Got hurt ({damage}), remaining health: {this.CurrentHealth}");

            // Fire "died" event -- only once.
            if (!this.hasDied &&
                this.CurrentHealth <= 0)
            {
                this.hasDied = true;
                this.Died?.Invoke();
            }
        }

        private void Awake()
        {
            this.CurrentHealth = this.MaxHealth;
        }
    }
}
