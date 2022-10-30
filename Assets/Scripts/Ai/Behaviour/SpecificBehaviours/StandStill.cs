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
            stopped = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (!stopped)
            {
                characterAccess.WalkTo(characterAccess.Character.Position);
                stopped = true;
            }
        }
    }
}
