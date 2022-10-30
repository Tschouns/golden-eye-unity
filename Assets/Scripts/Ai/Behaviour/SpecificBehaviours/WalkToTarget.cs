using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Walks toward a target.
    /// </summary>
    public class WalkToTarget : IBehaviour
    {
        private readonly Vector3 destination;

        public WalkToTarget(Vector3 destination)
        {
            this.destination = destination;

            Description = $"Walk to ({destination})";
        }

        public string Description { get; }

        public bool IsDone { get; private set; } = false;

        public void Update(ICharacterAccess characterAccess)
        {
            if (Vector3.Distance(characterAccess.Character.Position, destination) < 0.5f)
            {
                IsDone = true;
                return;
            }

            if (characterAccess.CurrentDestination != destination)
            {
                characterAccess.WalkTo(destination);
            }
        }

        public void Reset()
        {
            IsDone = false;
        }
    }
}
