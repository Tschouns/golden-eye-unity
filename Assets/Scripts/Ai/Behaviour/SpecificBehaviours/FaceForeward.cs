
namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character continuously faces forward, i.e. in walking direction.
    /// </summary>
    public class FaceForeward : IBehaviour
    {
        public string Description { get; } = "Face Forward.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            characterAccess.TurnAhead();
        }
    }
}