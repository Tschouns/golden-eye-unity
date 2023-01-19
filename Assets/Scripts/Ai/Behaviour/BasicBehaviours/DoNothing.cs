namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Doesn't do anything.
    /// </summary>
    public class DoNothing : IBehaviour
    {
        public string Description { get; } = "Do nothing.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
        }
    }
}