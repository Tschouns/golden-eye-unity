
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    public class ForgetHeardNoisesAfterSeconds : IBehaviour
    {
        private float forgetAfterSeconds;

        public ForgetHeardNoisesAfterSeconds(float forgetAfterSeconds)
        {
            this.forgetAfterSeconds = forgetAfterSeconds;
        }

        public string Description => $"Forget noises after {this.forgetAfterSeconds}s.";

        public bool IsDone => false;

        public void Reset()
        {
        }

        public void Update(ICharacterAccess characterAccess)
        {
            foreach (var noise in characterAccess.Memory.NoisesHeard)
            {
                if (Time.time - noise.GameTime > this.forgetAfterSeconds)
                {
                    characterAccess.Memory.NoisesHeard.Remove(noise);
                    break;
                }
            }
        }
    }
}