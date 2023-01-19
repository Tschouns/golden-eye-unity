namespace Assets.Scripts.Ai.Behaviour
{
    /// <summary>
    /// Represents a general behaviour of an NPC. Can be updated. Acts upon the charater itself.
    /// </summary>
    public interface IBehaviour
    {
        /// <summary>
        /// Gets a basic human-readable description of this behaviour.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether the behaviour is done.
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// Resets the behaviour, so it can be savely repeated.
        /// </summary>
        void Reset();

        /// <summary>
        /// Updates the behaviour.
        /// </summary>
        /// <param name="characterAccess">
        /// The character access instance, allowing the behaviour to act upon the character
        /// </param>
        void Update(ICharacterAccess characterAccess, float deltaTime);
    }
}
