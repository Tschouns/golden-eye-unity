using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Configuration;
using Assets.Scripts.Ai.Memory;
using Assets.Scripts.Ai.Navigation;
using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using Assets.Scripts.Damage;
using Assets.Scripts.Misc;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Ai
{
    /// <summary>
    /// Implements the A.I. of an NPC.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterAi : MonoBehaviour, INotifyOnDied
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
        private AbstractPeacefulBehaviourProvider peacefulBehaviour;

        [SerializeField]
        private AbstractAlertBehaviourProvider alertBehaviour;

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

        /// <summary>
        /// OBSOLETE: no longer has any effect.
        /// </summary>
        [SerializeField]
        private PatrolPath patrolPath;

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
            Debug.Assert(this.thisCharacter != null);
            Debug.Assert(this.eyeMovement != null);
            Debug.Assert(this.gunHandler != null);
            Debug.Assert(this.navMeshAgent != null);
            Debug.Assert(this.peacefulBehaviour != null);
            Debug.Assert(this.alertBehaviour != null);

            this.navMeshAgent.angularSpeed = this.angularSpeed;

            // Setup the "character access" -- properties of the character the behaviour can access.
            var characterManager = FindObjectOfType<CharacterManager>();
            Debug.Assert(characterManager != null);

            var escapePointManager = FindObjectOfType<EscapePointManager>();

            this.perception = new PerceptionImpl(this.thisCharacter, characterManager, () => this.fieldOfView);
            this.memory = new MemoryImpl(escapePointManager);
            this.characterAccess = new CharacterAccess(this);

            // Setup behaviour.
            var peaceful = this.peacefulBehaviour.GetPeacefulBehaviour();
            var alert = this.alertBehaviour.GetAlertBehaviour();

            this.behaviour = BehaviourFactory.CreateOrchestratedNpcBehaviour(peaceful, alert, this.timeToSpot);
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
            private readonly CharacterAi ai;

            public CharacterAccess(CharacterAi ai)
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
                this.ai.navMeshAgent.isStopped = false;

                _ = this.ai.navMeshAgent.SetDestination(destination);
            }

            public void RunTo(Vector3 destination)
            {
                this.ai.navMeshAgent.speed = this.ai.runnngSpeed;
                this.ai.navMeshAgent.isStopped = false;

                _ = this.ai.navMeshAgent.SetDestination(destination);
            }

            public void Stop()
            {
                this.ai.navMeshAgent.speed = this.ai.walkingSpeed;
                this.ai.navMeshAgent.isStopped = true;
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
