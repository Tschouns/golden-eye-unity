namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Represents a gun which shoots. Exposes methods necessary to manipulate the gun.
    /// </summary>
    public interface IGun
    {
        /// <summary>
        /// Gets a string which uniquely identifies the type of gun.
        /// </summary>
        string UniqueName { get; }

        /// <summary>
        /// Pulls (or holds) the trigger on the gun.
        /// </summary>
        void Trigger();
    }
}
