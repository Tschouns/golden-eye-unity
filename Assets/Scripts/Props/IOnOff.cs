
namespace Assets.Scripts.Props
{
    /// <summary>
    /// Represents an object which can be switched on and off.
    /// </summary>
    public interface IOnOff
    {
        /// <summary>
        /// Gets or sets a value inidicating whether the object is switched on or off.
        /// </summary>
        bool IsOn { get; set; }
    }
}
