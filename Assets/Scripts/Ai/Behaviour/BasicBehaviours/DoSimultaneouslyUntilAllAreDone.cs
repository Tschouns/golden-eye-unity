using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Performs multiple tasks simulatneouly, until all of the are done.
    /// </summary>
    public class DoSimultaneouslyUntilAllAreDone : IBehaviour
    {
        private readonly IReadOnlyList<IBehaviour> behaviours;

        public DoSimultaneouslyUntilAllAreDone(params IBehaviour[] behaviours)
        {
            Debug.Assert(behaviours != null);

            this.behaviours = behaviours;
            Description = $"Do simultaneously until all done: {string.Join(", ", this.behaviours.Select(b => $"[{b}]"))}";
        }
        public string Description { get; }

        public bool IsDone => behaviours.All(b => b.IsDone);

        public void Reset()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Reset();
            }
        }

        public void Update(ICharacterAccess characterAccess)
        {
            foreach (var behaviour in behaviours.Where(b => !b.IsDone))
            {
                behaviour.Update(characterAccess);
            }
        }
    }
}