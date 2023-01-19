using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Looks ahead, i.e. in the direction they're moving.
    /// </summary>
    public class FaceDirection : IBehaviour
    {
        private readonly Vector3 direction;

        public FaceDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        public string Description { get; } = "Face direction.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            characterAccess.TurnTowardsPoint(characterAccess.Character.Position + this.direction);
        }
    }
}