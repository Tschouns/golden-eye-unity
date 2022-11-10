namespace Assets.Scripts.Player
{
    /// <summary>
    /// Exposes movement properties.
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Gets or sets the walking speed.
        /// </summary>
        float WalkingSpeed { get; set; }

        /// <summary>
        /// Gets or sets the running speed.
        /// </summary>
        float RunningSpeed { get; set; }
    }
}
