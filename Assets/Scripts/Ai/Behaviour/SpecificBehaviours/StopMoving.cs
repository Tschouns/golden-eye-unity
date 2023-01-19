namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Stands still.
    /// </summary>
    public class StopMoving : IBehaviour
    {
        public string Description => "Stop moving.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (!this.IsDone)
            {
                characterAccess.Stop();
                this.IsDone = true;
            }
        }
    }
}
