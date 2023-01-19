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

            this.Description = $"Walk to ({destination})";
        }

        public string Description { get; }

        public bool IsDone { get; private set; } = false;

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (Vector3.Distance(characterAccess.Character.Position, this.destination) < 0.5f)
            {
                this.IsDone = true;
                return;
            }

            if (characterAccess.CurrentDestination != this.destination)
            {
                characterAccess.WalkTo(this.destination);
            }
        }

        public void Reset()
        {
            this.IsDone = false;
        }
    }
}
