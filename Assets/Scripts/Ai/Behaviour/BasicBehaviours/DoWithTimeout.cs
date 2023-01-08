using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// Continuously performs a task until a specified timeout is reached -- or the task itself is finished.
    /// </summary>
    public class DoWithTimeout : IBehaviour
    {
        private readonly IBehaviour timedBehaviour;
        private readonly float timeout;

        private float timeRunning;

        public DoWithTimeout(IBehaviour timedBehaviour, float timeout)
        {
            Debug.Assert(timedBehaviour != null);

            this.timedBehaviour = timedBehaviour;
            this.timeout = timeout;

            this.Description = $"Do with timeout ({this.timeout}s): [{this.timedBehaviour}]";
        }

        public string Description { get; }

        public bool IsDone => this.timedBehaviour.IsDone || this.timeRunning >= this.timeout;

        public void Reset()
        {
            this.timedBehaviour.Reset();
            this.timeRunning = 0;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            this.timedBehaviour.Update(characterAccess);
            this.timeRunning += Time.deltaTime;
        }
    }
}