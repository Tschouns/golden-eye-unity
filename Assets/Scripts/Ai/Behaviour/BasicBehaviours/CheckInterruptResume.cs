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

            Description = $"Check-Interrupt-Resume (base: [{baseBehaviour.Description}], check: [{checkBehaviour.Description}], interlude: [{interludeBehaviour.Description}])";
        }

        public string Description { get; }

        public bool IsDone => baseBehaviour.IsDone;

        public void Reset()
        {
            baseBehaviour.Reset();
            checkBehaviour.Reset();
            interludeBehaviour.Reset();

            isInterlude = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (isInterlude)
            {
                if (interludeBehaviour.IsDone)
                {
                    // Resume base task. Restart check task.
                    checkBehaviour.Reset();
                    interludeBehaviour.Reset();
                    isInterlude = false;

                    return;
                }

                interludeBehaviour.Update(characterAccess);
            }
            else
            {
                if (checkBehaviour.IsDone)
                {
                    // Start interlude.
                    checkBehaviour.Reset();
                    interludeBehaviour.Reset();
                    isInterlude = true;

                    return;
                }

                baseBehaviour.Update(characterAccess);
                checkBehaviour.Update(characterAccess);
            }
        }
    }
}