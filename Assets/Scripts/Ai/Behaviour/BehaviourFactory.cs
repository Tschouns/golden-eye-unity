using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;
using Assets.Scripts.Noise;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour
{
    /// <summary>
    /// Creates and orchestrates different possible behaviours.
    /// </summary>
    public static class BehaviourFactory
    {
        /// <summary>
        /// Creates an orchestrated NPC behaviour, where characters can be in "peaceful" or "alert" mode.
        /// </summary>
        /// <param name="peacefulTask">
        /// The specific task a character performs while "peaceful"
        /// </param>
        /// <param name="alertTask">
        /// The specific task a character performs while "alert"
        /// </param>
        /// <param name="timeToSpot">
        /// The time it takes the AI to "spot" (i.e. identify) an enemy in view
        /// </param>
        /// <returns>
        /// The orchestrates NPC behaviour
        /// </returns>
        public static IBehaviour CreateOrchestratedNpcBehaviour(IBehaviour peacefulTask, IBehaviour alertTask, float timeToSpot)
        {
            Debug.Assert(peacefulTask != null);
            Debug.Assert(alertTask != null);

            var completePeacefulBehaviour = new DoWhile(
                new DoSimultaneouslyUntilAllAreDone(
                    CreateLookAroundRelaxed(),
                    new CycleThrough(
                        new DoWhile(peacefulTask, c => !c.Memory.NoisesHeard.Any(), "no noises heard"),
                        new DoWithTimeout(new InvestigateNoise(), 15f),
                        new DoWithTimeout(new LookAhead(), 2f))),
                c => !HasAnyThreats(c),
                "has no threats");

            var orchestratedBehaviour = new DoSimultaneouslyUntilAllAreDone(
                new ListenForNoise(),
                new ForgetHeardNoisesAfterSeconds(10),
                new SpotEnemies(() => timeToSpot),
                new CycleThrough(
                    new FaceForeward(),
                    completePeacefulBehaviour,
                    new DoSimultaneouslyUntilEitherIsDone(
                        new LookAtClosestVisibleAliveTarget(),
                        alertTask)));

            return orchestratedBehaviour;
        }

        /// <summary>
        /// Creates a behaviour where an NPC will stand guard at (and return to) a specific position.
        /// </summary>
        /// <param name="position">
        /// The character's guard position
        /// </param>
        /// <param name="faceDirection">
        /// The character's looking direction while guarding
        /// </param>
        /// <returns>
        /// The NPC behaviour
        /// </returns>
        public static IBehaviour CreateStandGuardAtPositionBehaviour(Vector3 position, Vector3 faceDirection)
        {
            var behaviour = new DoSimultaneouslyUntilAllAreDone(
                CreateLookAroundRelaxed(),
                new DoInOrder(
                    new WalkToTarget(position),
                    new DoWithTimeout(new FaceDirection(faceDirection), 3f),
                    new FaceForeward()));

            return behaviour;
        }

        /// <summary>
        /// Creates a behaviour where an NPC will patrol along a specified path.
        /// </summary>
        /// <param name="patrolPointPositions">
        /// The patrol point positions
        /// </param>
        /// <returns>
        /// The NPC behaviour
        /// </returns>
        public static IBehaviour CreatePatrolAlongPathBehaviour(Vector3[] patrolPointPositions)
        {
            var patrolBehaviour = new PatrolEndToEnd(patrolPointPositions);

            return patrolBehaviour;
        }

        /// <summary>
        /// Creates a behaviour where an NPC will investigate gunshots and engage active targets.
        /// </summary>
        /// <returns>
        /// The NPC behaviour
        /// </returns>
        public static IBehaviour CreateEngageActiveTargetsBehaviour()
        {
            var fire = new CycleThrough(
                new DoWithTimeout(new Shoot(), 1.2f),
                new DoWithTimeout(new DoNothing(), 1f));

            var shootAndReloadOnce = new DoInOrder(
                new DoWhile(
                    fire,
                    c =>
                        c.GunHandler.Gun.CurrentNumberOfBullets > 0 &&
                        HasVisibleAliveTarget(c),
                    "gun has bullets"),
                new ReloadGun());

            var shootAndReloadContinuously = new CycleThrough(
                shootAndReloadOnce,
                new DoWithTimeout(new DoNothing(), 1f));

            var engageClosestTarget = new DoSimultaneouslyUntilAllAreDone(
                    new FaceClosestVisibleTarget(),
                    new CycleThrough(
                        shootAndReloadOnce,
                        new DoWhile(
                            new RunTowardsClosestTarget(),
                            c => !HasVisibleAliveTarget(c), 
                            "while target is not visible"),
                        new DoWithTimeout(new RunTowardsClosestTarget(), 0.5f),
                        new StopMoving()));

            var engageAllTargets = new DoWhile(
                engageClosestTarget,
                HasAliveTarget,
                "any visible alive targets");

            var alertBehaviour = new DoInOrder(
                new EquipGun(),
                new DoWhile(
                    new InvestigateNoise(),
                    c => !HasVisibleAliveTarget(c),
                    "any alive targets"),
                engageAllTargets,
                new DoWhile(new DoNothing(), HasHeardAnyGunshots, "any gunshots heard"),
                new DoWithTimeout(new DoNothing(), 3f),
                new UnequipGun());

            return alertBehaviour;
        }

        /// <summary>
        /// Creates an "alert" behaviour where an NPC will escape to an escape point.
        /// </summary>
        /// <returns>
        /// The NPC behaviour
        /// </returns>
        public static IBehaviour CreateEscapeBehaviour()
        {
            var escapeBehaviour = new CycleThrough(
                new FaceForeward(),
                new EscapeToEscapePoint(),
                new DoWhile(
                    new DoNothing(),
                    c => !HasVisibleAliveTarget(c),
                    "no enemy in sight"));

            return escapeBehaviour;
        }

        private static IBehaviour CreateLookAroundRelaxed()
        {
            var lookAroundRelaxed = new CycleThrough(
                new DoWithTimeout(new LookAhead(), 3),
                new DoWithTimeout(new LookAtClosestVisibleCharacter(), 5));

            return lookAroundRelaxed;
        }

        private static bool HasAliveTarget(ICharacterAccess a)
        {
            return a.Memory.ActiveTargets.Any(c => c.IsAlive);
        }

        private static bool HasVisibleAliveTarget(ICharacterAccess a)
        {
            return a.Perception.CharactersInView.Any(c =>
                    c.IsAlive &&
                    a.Memory.ActiveTargets.Contains(c));
        }

        private static bool HasHeardAnyGunshots(ICharacterAccess c)
        {
            var hasHeardAnyGunshots = c.Memory.NoisesHeard.Any(n => n.Type == NoiseType.GunShot);

            return hasHeardAnyGunshots;
        }

        private static bool HasAnyThreats(ICharacterAccess c)
        {
            return
                HasAliveTarget(c) ||
                HasHeardAnyGunshots(c);
        }
    }
}