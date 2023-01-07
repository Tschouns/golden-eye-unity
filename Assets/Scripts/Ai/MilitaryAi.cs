using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;
using Assets.Scripts.Ai.Memory;
using Assets.Scripts.Ai.Patrols;
using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using Assets.Scripts.Damage;
using Assets.Scripts.Misc;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Ai
{
    /// <summary>
    /// Implements the A.I. of a military NPC.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class MilitaryAi : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private Character thisCharacter;

        [SerializeField]
        private EyeMovement eyeMovement;

        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private float walkingSpeed = 1.0f;

        [SerializeField]
        private float runnngSpeed = 3.0f;

        [SerializeField]
        private float angularSpeed = 120f;

        [SerializeField]
        private float fieldOfView = 120f;

        [SerializeField]
        private float timeToSpot = 2.0f;

        [SerializeField]
        private PatrolPath patrolPath;

        private ICharacterManager characterManager;
        private PerceptionImpl perception;
        private IMemory memory;
        private IBehaviour behaviour;
        private ICharacterAccess characterAccess;

        private Vector3? currentFocusPoint = null;

        public void NotifyOnDied()
        {
            this.enabled = false;

            this.navMeshAgent.isStopped = true;
            this.navMeshAgent.enabled = false;
        }

        private void Awake()
        {
            Debug.Assert(this.thisCharacter != null, "No character assigned to AI.");
            Debug.Assert(this.eyeMovement != null, "No eye movement assigned to AI.");
            Debug.Assert(this.gunHandler != null, "No gun handler assigned to AI.");
            Debug.Assert(this.navMeshAgent != null, "No nav mesh agent assigned to AI.");

            this.navMeshAgent.angularSpeed = this.angularSpeed;

            this.characterManager = FindObjectOfType<CharacterManager>();
            Debug.Assert(this.characterManager != null);

            this.perception = new PerceptionImpl(this.thisCharacter, this.characterManager, () => this.fieldOfView);
            this.memory = new MemoryImpl();
            this.characterAccess = new CharacterAccess(this);

            IBehaviour peacefulBehaviour = new DoNothing();

            if (this.patrolPath != null)
            {
                peacefulBehaviour = new PatrolEndToEnd(this.patrolPath.PatrolPoints.Select(p => p.Position).ToArray());
            }

            CycleThrough lookAroundBehaviour = new(
                new DoWithTimeout(new LookAhead(), 3),
                new DoWithTimeout(new LookAtClosestVisibleCharacter(), 5));

            DoWithTimeout engageBehaviour = new(
                new DoSimultaneouslyUntilAllAreDone(
                    new StandStill(),
                    new FaceClosestVisibleTarget(),
                    new CycleThrough(
                        new DoWithTimeout(new Shoot(), 1.5f),
                        new DoWithTimeout(new DoNothing(), 1f))),
                5f);

            this.behaviour = new CheckInterruptResume(
                    peacefulBehaviour,
                    new DoSimultaneouslyUntilEitherIsDone(
                        new SpotEnemy(() => this.timeToSpot),
                        lookAroundBehaviour),
                    engageBehaviour);
        }

        private void Update()
        {
            this.perception.Update();
            this.behaviour.Update(this.characterAccess);

            if (this.currentFocusPoint != null)
            {
                this.TurnTowards(this.currentFocusPoint.Value);
            }
        }

        /// <summary>
        /// Turns the entire NPC, and tilts the head, towards the specified target point.
        /// </summary>
        private void TurnTowards(Vector3 targetPoint)
        {
            var targetLookDirection = targetPoint - this.thisCharacter.Head.Position;
            var updatedLookDirection = TransformHelper.RotateTowardsAtSpeed(this.thisCharacter.Head.LookDirection, targetLookDirection, this.angularSpeed);

            // Tilt the characters head.
            updatedLookDirection.Normalize();
            float headTilt = -(Mathf.Asin(updatedLookDirection.y) * Mathf.Rad2Deg);
            this.thisCharacter.TiltHead(headTilt);

            // Turn the whole NPC around Y. Don't change elevation.
            updatedLookDirection.y = 0;
            this.transform.LookAt(this.transform.position + updatedLookDirection);
        }

        /// <summary>
        /// Adapter type to provide AI/behaviour components with access to the character itself.
        /// </summary>
        private class CharacterAccess : ICharacterAccess
        {
            private readonly MilitaryAi ai;

            public CharacterAccess(MilitaryAi ai)
            {
                Debug.Assert(ai != null);

                this.ai = ai;
            }

            public ICharacter Character => this.ai.thisCharacter;
            public IEyes Eyes => this.ai.eyeMovement;
            public IPerception Perception => this.ai.perception;
            public IGunHandler GunHandler => this.ai.gunHandler;
            public Vector3 CurrentDestination => this.ai.navMeshAgent.destination;
            public IMemory Memory => this.ai.memory;

            public void WalkTo(Vector3 destination)
            {
                this.ai.navMeshAgent.speed = this.ai.walkingSpeed;
                _ = this.ai.navMeshAgent.SetDestination(destination);
            }

            public void RunTo(Vector3 destination)
            {
                this.ai.navMeshAgent.speed = this.ai.runnngSpeed;
                _ = this.ai.navMeshAgent.SetDestination(destination);
            }

            public void TurnTowardsPoint(Vector3 targetPoint)
            {
                this.ai.currentFocusPoint = targetPoint;
                this.ai.navMeshAgent.angularSpeed = 0;
            }

            public void TurnAhead()
            {
                this.ai.thisCharacter.TiltHead(0);
                this.ai.currentFocusPoint = null;
                this.ai.navMeshAgent.angularSpeed = this.ai.angularSpeed;
            }
        }
    }
}
