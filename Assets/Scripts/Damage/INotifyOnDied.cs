namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Allows a component to be notified when a character dies.
    /// </summary>
    public interface INotifyOnDied
    {
        /// <summary>
        /// Notifies this component that the character has died.
        /// </summary>
        void NotifyOnDied();
    }
}
