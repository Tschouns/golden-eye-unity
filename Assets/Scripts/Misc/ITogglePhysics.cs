
namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Represents an object with physics which can switched on and off.
    /// </summary>
    public interface ITogglePhysics
    {
        /// <summary>
        /// Activates the object's physics.
        /// </summary>
        void ActivatePhysics();

        /// <summary>
        /// Deactivates the object's physics.
        /// </summary>
        void DeactivatePhysics();
    }
}
