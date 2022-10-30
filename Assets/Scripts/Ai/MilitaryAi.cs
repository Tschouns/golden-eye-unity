using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Behaviour.BasicBehaviours;
using Assets.Scripts.Ai.Behaviour.SpecificBehaviours;
using Assets.Scripts.Ai.Memory;
using Assets.Scripts.Ai.Patrols;
using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using Assets.Scripts.Damage;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Ai
{
    /// <summary>
    /// Implements the A.I. of a military NPC.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class MilitaryAi : MonoBehaviour
    {
        [SerializeField]
        private Character thisCharacter;

        [SerializeField]
        private GunHandler gunHandler;

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private Health health;

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

        private void Awake()
        {
            Debug.Assert(this.thisCharacter != null);
            Debug.Assert(this.navMeshAgent != null);
            Debug.Assert(this.health != null);

            this.navMeshAgent.angularSpeed = this.angularSpeed;
            this.health.Died += this.OnDied;

            this.characterManager = FindObjectOfType<CharacterManager>();
            Debug.Assert(this.characterManager != null);

            this.perception = new PerceptionImpl(thisCharacter, characterManager, () => this.fieldOfView);
            this.memory = new MemoryImpl();
            this.characterAccess = new CharacterAccess(this);

            IBehaviour peacefulBehaviour = new DoNothing();

            if (this.patrolPath != null)
            {
                peacefulBehaviour = new PatrolEndToEnd(this.patrolPath.PatrolPoints.Select(p => p.Position).ToArray());
            }

            var lookAroundBehaviour = new CycleThrough(
                new DoWithTimeout(new LookAhead(), 3),
                new DoWithTimeout(new LookAtClosestVisibleCharacter(), 5));

            var engageBehaviour = new DoWithTimeout(
                new DoSimultaneouslyUntilAllAreDone(
                    new StandStill(),
                    new LookAtClosestVisibleTarget(),
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
            if (!this.health.IsAlive)
            {
                return;
            }

            this.perception.Update();
            this.behaviour.Update(this.characterAccess);

            if (this.currentFocusPoint != null)
            {
                this.TurnTowards(this.currentFocusPoint.Value);
            }
        }

        private void OnDied()
        {
            this.navMeshAgent.isStopped = true;
        }

        private void TurnTowards(Vector3 targetPoint)
        {
            if (!this.health.IsAlive)
            {
                return;
            }

            var maxRadians = this.angularSpeed * Mathf.Deg2Rad * Time.deltaTime;
            var targetLookDirection = targetPoint - this.thisCharacter.Head.Position;

            // Turn around Y. Don't change elevation (yet).
            var updatedLookDirection = Vector3.RotateTowards(this.thisCharacter.Head.LookDirection, targetLookDirection, maxRadians, 1000);
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
            public IPerception Perception => this.ai.perception;
            public IGunHandler GunHandler => this.ai.gunHandler;
            public Vector3 CurrentDestination => this.ai.navMeshAgent.destination;
            public IMemory Memory => this.ai.memory;

            public void WalkTo(Vector3 destination)
            {
                this.ai.navMeshAgent.speed = this.ai.walkingSpeed;
                this.ai.navMeshAgent.SetDestination(destination);
            }

            public void RunTo(Vector3 destination)
            {
                this.ai.navMeshAgent.speed = this.ai.runnngSpeed;
                this.ai.navMeshAgent.SetDestination(destination);
            }

            public void LookAt(Vector3 targetPoint)
            {
                this.ai.currentFocusPoint = targetPoint;
                this.ai.navMeshAgent.angularSpeed = 0;
            }

            public void LookAhead()
            {
                this.ai.currentFocusPoint = null;
                this.ai.navMeshAgent.angularSpeed = this.ai.angularSpeed;
            }
        }
    }
}
