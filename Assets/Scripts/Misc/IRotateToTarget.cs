namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Represents a component which rotates to a specified target rotation at a constant speed.
    /// </summary>
    public interface IRotateToTarget
    {
        /// <summary>
        /// Gets or sets the angular speed.
        /// </summary>
        float AngularSpeed { get; set; }

        /// <summary>
        /// Sets the rotation target.
        /// </summary>
        /// <param name="rotationDeg">
        /// The rotation target in degrees.
        /// </param>
        void SetRotationTarget(float rotationDeg);
    }
}
