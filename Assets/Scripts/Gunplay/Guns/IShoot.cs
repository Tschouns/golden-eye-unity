namespace Assets.Scripts.Gunplay.Guns
{
    /// <summary>
    /// Represents a gun which shoots. Exposes methods necessary to manipulate the gun.
    /// </summary>
    public interface IShoot
    {
        /// <summary>
        /// Pulls (or holds) the trigger on the gun.
        /// </summary>
        void Trigger();
    }
}
