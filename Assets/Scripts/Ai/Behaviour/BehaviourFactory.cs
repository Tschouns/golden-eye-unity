
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
        public static IBehaviour CreateSoldierBehaviour(IPatrolPath patrolPathOrNull, float timeToSpot)
        {
            IBehaviour baseTask = new DoNothing();

            if (patrolPathOrNull != null)
            {
                baseTask = new PatrolEndToEnd(patrolPathOrNull.PatrolPoints.Select(p => p.Position).ToArray());
            }

            // "Peaceful" mode.
            var peacefulBehaviour = new DoSimultaneouslyUntilAllAreDone(
                new CycleThrough(
                    new DoWithTimeout(baseTask, 12f),
                    new DoWithTimeout(new StandStill(), 3f)),
                new FaceForeward());

            // "Alert" mode.
            var engage = new DoSimultaneouslyUntilAllAreDone(
                    new StandStill(),
                    new FaceClosestVisibleTarget(),
                    new CycleThrough(
                        new DoWithTimeout(new Shoot(), 1.5f),
                        new DoWithTimeout(new DoNothing(), 1f)));

            var alertBehaviour = new DoWhile(
                engage,
                c => c.Memory.ActiveTargets.Any(c => c.IsAlive),
                "any active targets");

            // Complete orchestrated behaviour.
            var behaviour = new CheckInterruptResume(
                    peacefulBehaviour,
                    new DoSimultaneouslyUntilEitherIsDone(
                        new SpotEnemy(() => timeToSpot),
                        CreateLookAroundRelaxed()),
                    alertBehaviour);

            return behaviour;
        }

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

            // Engage behaviour.
            var engageBehaviour = new DoWithTimeout(
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
                        CreateLookAroundRelaxed()),
                    engageBehaviour);

            return behaviour;
        }

        private static IBehaviour CreateLookAroundRelaxed()
        {
            var lookAroundRelaxed = new CycleThrough(
                new DoWithTimeout(new LookAhead(), 3),
                new DoWithTimeout(new LookAtClosestVisibleCharacter(), 5));

            return lookAroundRelaxed;
        }
    }
}