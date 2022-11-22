namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Looks ahead, i.e. in the direction they're moving.
    /// </summary>
    public class LookAhead : IBehaviour
    {
        public string Description { get; } = "Look ahead.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            characterAccess.LookAhead();
        }
    }
}