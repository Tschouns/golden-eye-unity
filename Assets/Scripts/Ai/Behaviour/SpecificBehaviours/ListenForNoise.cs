
namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Continuously listens for noise events, and adds them to the memory.
    /// </summary>
    public class ListenForNoise : IBehaviour
    {
        public string Description => "Listen for noise.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (characterAccess.Perception.TryDequeNoise(out var noise))
            {
                characterAccess.Memory.NoisesHeard.Add(noise);
            }
        }
    }
}
