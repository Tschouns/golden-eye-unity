using Assets.Scripts.Noise;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character investigates noise events.
    /// </summary>
    public class InvestigateNoise : IBehaviour
    {
        public string Description => "Investigate noise.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (this.IsDone)
            {
                return;
            }

            if (!characterAccess.Memory.NoisesHeard.Any())
            {
                this.IsDone = true;
                return;
            }

            // Prioritize.
            if (characterAccess.Memory.NoisesHeard.Count > 1)
            {
                var mostImportantNoise = characterAccess.Memory.NoisesHeard
                    .OrderByDescending(n => n.Type)
                    .ThenBy(n => Vector3.Distance(n.PointOfOrigin, characterAccess.Character.Head.Position))
                    .First();

                characterAccess.Memory.NoisesHeard.Clear();
                characterAccess.Memory.NoisesHeard.Add(mostImportantNoise);
            }

            // Walk/run towards "most important" noise.
            var noise = characterAccess.Memory.NoisesHeard.First();
            if (noise.Type >= NoiseType.GunShot)
            {
                characterAccess.RunTo(noise.PointOfOrigin);
            }
            else
            {
                characterAccess.WalkTo(noise.PointOfOrigin);
            }

            // Stop when close.
            if (Vector3.Distance(characterAccess.Character.Position, noise.PointOfOrigin) < 3)
            {
                var lookAtPoint = new Vector3(
                    noise.PointOfOrigin.x,
                    characterAccess.Character.Head.Position.y,
                    noise.PointOfOrigin.z);

                characterAccess.Stop();
                characterAccess.TurnTowardsPoint(lookAtPoint);

                characterAccess.Memory.NoisesHeard.Clear();
                this.IsDone = true;
            }
        }
    }
}
