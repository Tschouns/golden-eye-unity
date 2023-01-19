
namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously faces forward, i.e. in walking direction.
    /// </summary>
    public class FaceForeward : IBehaviour
    {
        public string Description { get; } = "Face Forward.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            characterAccess.TurnAhead();
            this.IsDone = true;
        }
    }
}