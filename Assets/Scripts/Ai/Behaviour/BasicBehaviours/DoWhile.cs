using System;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// Performs a task while a certain condition is met -- or the task itself is finished.
    /// </summary>
    public class DoWhile : IBehaviour
    {
        private readonly IBehaviour baseBehaviour;
        private readonly Func<ICharacterAccess, bool> condition;
        private readonly string conditionDescription;

        public DoWhile(IBehaviour baseBehaviour, Func<ICharacterAccess, bool> condition, string conditionDescription)
        {
            Debug.Assert(baseBehaviour != null);
            Debug.Assert(condition != null);
            Debug.Assert(conditionDescription != null);

            this.baseBehaviour = baseBehaviour;
            this.condition = condition;
            this.conditionDescription = conditionDescription;
        }

        public string Description => $"Do while \"{conditionDescription}\": {this.baseBehaviour}";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.baseBehaviour.Reset();
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            var evatualtion = this.condition(characterAccess);
            if (!evatualtion || this.baseBehaviour.IsDone)
            {
                this.IsDone = true;
                return;
            }

            this.baseBehaviour.Update(characterAccess, deltaTime);
        }
    }
}