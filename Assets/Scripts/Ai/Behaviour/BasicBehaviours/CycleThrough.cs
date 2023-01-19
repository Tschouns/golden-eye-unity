using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Continuously cycles through a list of behaviours.
    /// </summary>
    public class CycleThrough : IBehaviour
    {
        private readonly IBehaviour sequence;

        public CycleThrough(params IBehaviour[] behaviours)
        {
            Debug.Assert(behaviours != null);

            this.sequence = new DoInOrder(behaviours);
            this.Description = $"Cycle through: {string.Join(", ", behaviours.Select(b => $"[{b.Description}]"))}";
        }
        public bool IsDone => false;

        public string Description { get; }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (this.sequence.IsDone)
            {
                this.sequence.Reset();
            }

            this.sequence.Update(characterAccess, deltaTime);
        }

        public void Reset()
        {
            this.sequence.Reset();
        }
    }
}
