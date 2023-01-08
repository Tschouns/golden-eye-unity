namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Stands still.
    /// </summary>
    public class StandStill : IBehaviour
    {
        private bool stopped = false;

        public string Description => "Stand still.";

        public bool IsDone => false;

        public void Reset()
        {
            this.stopped = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (!this.stopped)
            {
                characterAccess.WalkTo(characterAccess.Character.Position);
                this.stopped = true;
            }
        }
    }
}
