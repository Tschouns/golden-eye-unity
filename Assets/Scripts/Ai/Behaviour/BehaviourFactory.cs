
using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;
using Assets.Scripts.Ai.Patrols;
using System.Linq;

namespace Assets.Scripts.Ai.Behaviour
{
    /// <summary>
    /// Creates and orchestrates different possible behaviours.
    /// </summary>
    public static class BehaviourFactory
    {
        /// <summary>
        /// Creates the original stupid "military AI" prototype behaviour.
        /// </summary>
        /// <param name="patrolPathOrNull">
        /// An optional patrol path
        /// </param>
        /// <param name="timeToSpot">
        /// The time it takes the AI to "spot" (i.e. identify) an enemy in view
        /// </param>
        /// <returns>
        /// The complete behaviour
        /// </returns>
        public static IBehaviour CreateLegacyMilitaryAiBehaviour(IPatrolPath patrolPathOrNull, float timeToSpot)
        {
            // Peacful behaviour.
            IBehaviour peacefulBehaviour = new DoNothing();

            if (patrolPathOrNull != null)
            {
                peacefulBehaviour = new PatrolEndToEnd(patrolPathOrNull.PatrolPoints.Select(p => p.Position).ToArray());
            }

            CycleThrough lookAroundBehaviour = new(
                new DoWithTimeout(new LookAhead(), 3),
                new DoWithTimeout(new LookAtClosestVisibleCharacter(), 5));

            // Engage behaviour.
            DoWithTimeout engageBehaviour = new(
                new DoSimultaneouslyUntilAllAreDone(
                    new StandStill(),
                    new FaceClosestVisibleTarget(),
                    new CycleThrough(
                        new DoWithTimeout(new Shoot(), 1.5f),
                        new DoWithTimeout(new DoNothing(), 1f))),
                5f);

            // Complete orchestrated behaviour.
            var behaviour = new CheckInterruptResume(
                    peacefulBehaviour,
                    new DoSimultaneouslyUntilEitherIsDone(
                        new SpotEnemy(() => timeToSpot),
                        lookAroundBehaviour),
                    engageBehaviour);

            return behaviour;
        }
    }
}