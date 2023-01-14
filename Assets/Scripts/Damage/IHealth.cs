using System;

namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Exposes information on an entity's health.
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// Is fired when the health has changed.
        /// </summary>
        event Action HealthChanged;

        /// <summary>
        /// Is fired when the entity has died.
        /// </summary>
        event Action Died;

        /// <summary>
        /// Gets the maximum health.
        /// </summary>
        int MaxHealth { get; }

        /// <summary>
        /// Gets the current health.
        /// </summary>
        int CurrentHealth { get; }

        /// <summary>
        /// Gets a value indicating whether the entity is alive.
        /// </summary>
        bool IsAlive { get; }
    }
}
