using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Performs a list of tasks in sequence.
    /// </summary>
    public class DoInOrder : IBehaviour
    {
        private readonly IReadOnlyList<IBehaviour> steps;
        private int current = 0;

        public DoInOrder(params IBehaviour[] behaviours)
        {
            Debug.Assert(behaviours != null);

            steps = behaviours;
            Description = $"Sequence through: {string.Join(", ", behaviours.Select(b => $"[{b.Description}]"))}";
        }

        public string Description { get; }

        public bool IsDone => current >= steps.Count;

        private IBehaviour CurrentStep => steps[current];

        public void Update(ICharacterAccess characterAccess)
        {
            if (IsDone)
            {
                return;
            }

            CurrentStep.Update(characterAccess);

            if (CurrentStep.IsDone)
            {
                Debug.Log($"Step done: {CurrentStep.Description}");
                current++;

                if (!IsDone)
                {
                    Debug.Log($"Next step: {CurrentStep.Description}");
                }
            }
        }

        public void Reset()
        {
            foreach (var behaviour in steps)
            {
                behaviour.Reset();
            }

            current = 0;
        }
    }
}
