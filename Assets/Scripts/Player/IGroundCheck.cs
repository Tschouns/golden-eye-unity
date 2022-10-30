namespace Assets.Scripts.Player
{
    /// <summary>
    /// Provides indication of whether the object is grounded.
    /// </summary>
    public interface IGroundCheck
    {
        /// <summary>
        /// Gets a value indicating whether the object is currently grounded.
        /// </summary>
        bool IsGrounded { get; }
    }
}
