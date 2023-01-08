using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.BasicBehaviours
{
    /// <summary>
    /// A character simultaneously performs a "base" and a "check" task.
    /// As soon as the "check" task is done, it will interrupt the "base" task and perform an "interlude" task.
    /// After the "interlude" task is done it will resume performing the "base" task, and simoultaneously restart the "check" task.
    /// So the "base" task can be interrupted multiple times -- and the "interlude" task performed multiple times -- before the "base" task is finished.
    /// </summary>
    public class CheckInterruptResume : IBehaviour
    {
        private readonly IBehaviour baseBehaviour;
        private readonly IBehaviour checkBehaviour;
        private readonly IBehaviour interludeBehaviour;

        private bool isInterlude = false;

        public CheckInterruptResume(IBehaviour baseBehaviour, IBehaviour checkBehaviour, IBehaviour interludeBehaviour)
        {
            Debug.Assert(baseBehaviour != null);
            Debug.Assert(checkBehaviour != null);
            Debug.Assert(interludeBehaviour != null);

            this.baseBehaviour = baseBehaviour;
            this.checkBehaviour = checkBehaviour;
            this.interludeBehaviour = interludeBehaviour;

            this.Description = $"Check-Interrupt-Resume (base: [{baseBehaviour.Description}], check: [{checkBehaviour.Description}], interlude: [{interludeBehaviour.Description}])";
        }

        public string Description { get; }

        public bool IsDone => this.baseBehaviour.IsDone;

        public void Reset()
        {
            this.baseBehaviour.Reset();
            this.checkBehaviour.Reset();
            this.interludeBehaviour.Reset();

            this.isInterlude = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (this.isInterlude)
            {
                if (this.interludeBehaviour.IsDone)
                {
                    // Resume base task. Restart check task.
                    this.checkBehaviour.Reset();
                    this.interludeBehaviour.Reset();
                    this.isInterlude = false;

                    return;
                }

                this.interludeBehaviour.Update(characterAccess);
            }
            else
            {
                if (this.checkBehaviour.IsDone)
                {
                    // Start interlude.
                    this.checkBehaviour.Reset();
                    this.interludeBehaviour.Reset();
                    this.isInterlude = true;

                    return;
                }

                this.baseBehaviour.Update(characterAccess);
                this.checkBehaviour.Update(characterAccess);
            }
        }
    }
}