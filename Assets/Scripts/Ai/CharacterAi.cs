﻿using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Ai.Configuration;
using Assets.Scripts.Ai.Memory;
using Assets.Scripts.Ai.Navigation;
using Assets.Scripts.Ai.Perception;
using Assets.Scripts.Characters;
using Assets.Scripts.Damage;
using Assets.Scripts.Misc;
using Assets.Scripts.Noise;
using System.Collections;
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

        private PerceptionImpl perception;
        private IMemory memory;
        private IBehaviour behaviour;
        private ICharacterAccess characterAccess;

        private Vector3? currentFocusPoint = null;
        private float lastUpdateTime = 0;

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
            var noiseEventBus = FindObjectOfType<NoiseEventBus>();
            Debug.Assert(noiseEventBus != null);

            var characterManager = FindObjectOfType<CharacterManager>();
            Debug.Assert(characterManager != null);

            var escapePointManager = FindObjectOfType<EscapePointManager>();
            Debug.Assert(escapePointManager != null);

            this.perception = new PerceptionImpl(this.thisCharacter, noiseEventBus, characterManager, () => this.fieldOfView);
            this.memory = new MemoryImpl(escapePointManager);
            this.characterAccess = new CharacterAccess(this);

            // Setup behaviour.
            var peaceful = this.peacefulBehaviour.GetPeacefulBehaviour();
            var alert = this.alertBehaviour.GetAlertBehaviour();

            this.behaviour = BehaviourFactory.CreateOrchestratedNpcBehaviour(peaceful, alert, this.timeToSpot);

            // Start AI update.
            this.StartCoroutine(this.UpdateAiStuff());
        }

        private void Update()
        {
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

        private IEnumerator UpdateAiStuff()
        {
            // Initialize.
            this.lastUpdateTime = Time.time;

            // Random offset.
            yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

            while (this.enabled)
            {
                var delta = Time.time - this.lastUpdateTime;
                this.lastUpdateTime = Time.time;

                // Update AI stuff.
                this.perception.Update();
                this.behaviour.Update(this.characterAccess, delta);

                yield return new WaitForSeconds(0.1f);
            }
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
