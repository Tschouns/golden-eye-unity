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

            sequence = new DoInOrder(behaviours);
            Description = $"Cycle through: {string.Join(", ", behaviours.Select(b => $"[{b.Description}]"))}";
        }
        public bool IsDone => false;

        public string Description { get; }

        public void Update(ICharacterAccess characterAccess)
        {
            if (sequence.IsDone)
            {
                sequence.Reset();
            }

            sequence.Update(characterAccess);
        }

        public void Reset()
        {
            sequence.Reset();
        }
    }
}
