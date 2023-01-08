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

            this.steps = behaviours;
            this.Description = $"Sequence through: {string.Join(", ", behaviours.Select(b => $"[{b.Description}]"))}";
        }

        public string Description { get; }

        public bool IsDone => this.current >= this.steps.Count;

        private IBehaviour CurrentStep => this.steps[this.current];

        public void Update(ICharacterAccess characterAccess)
        {
            if (this.IsDone)
            {
                return;
            }

            this.CurrentStep.Update(characterAccess);

            if (this.CurrentStep.IsDone)
            {
                Debug.Log($"Step done: {this.CurrentStep.Description}");
                this.current++;

                if (!this.IsDone)
                {
                    Debug.Log($"Next step: {this.CurrentStep.Description}");
                }
            }
        }

        public void Reset()
        {
            foreach (var behaviour in this.steps)
            {
                behaviour.Reset();
            }

            this.current = 0;
        }
    }
}
