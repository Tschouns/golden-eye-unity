using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Performs multiple tasks simulatneouly, until any of the tasks is done.
    /// </summary>
    public class DoSimultaneouslyUntilEitherIsDone : IBehaviour
    {
        private readonly IReadOnlyList<IBehaviour> behaviours;

        public DoSimultaneouslyUntilEitherIsDone(params IBehaviour[] behaviours)
        {
            Debug.Assert(behaviours != null);

            this.behaviours = behaviours;
            this.Description = $"Do simultaneously until any is done: {string.Join(", ", this.behaviours.Select(b => $"[{b}]"))}";
        }
        public string Description { get; }

        public bool IsDone => this.behaviours.Any(b => b.IsDone);

        public void Reset()
        {
            foreach (var behaviour in this.behaviours)
            {
                behaviour.Reset();
            }
        }

        public void Update(ICharacterAccess characterAccess)
        {
            foreach (var behaviour in this.behaviours.Where(b => !b.IsDone))
            {
                behaviour.Update(characterAccess);
            }
        }
    }
}