namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Provides boolean values for each action, indicating whether the action is being performed in the current frame.
    /// </summary>
    public interface IPlayerActions
    {
        bool Forward { get; }
        bool Backward { get; }
        bool Left { get; }
        bool Right { get; }
        bool Run { get; }
        bool ToggleCrouch { get; }
        bool Trigger { get; }
        bool Reload { get; }
        bool CycleWeapon { get; }
        bool TogglePause { get; }
    }
}
