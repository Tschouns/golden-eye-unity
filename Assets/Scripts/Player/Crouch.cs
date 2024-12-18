﻿using Assets.Scripts.Controls;
using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Reacts to player input and allows the player to toggle-crouch.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(FirstPersonMovement))]
    public class Crouch : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private Transform head;

        [SerializeField]
        private float crouchHeight = 1f;

        [SerializeField]
        private float crouchSpeedFactor = 0.5f;

        private CharacterController characterController;
        private IMove movement;

        private float originalControllerHeight;
        private float originalHeadHeight;

        private float originalWalkingSpeed;
        private float originalRunningSpeed;

        private bool isCrouching;

        public void NotifyOnDied()
        {
            this.enabled = false;
        }

        private void Awake()
        {
            Debug.Assert(this.head != null, "Head is not set.");

            this.characterController = this.GetComponent<CharacterController>();
            Debug.Assert(this.characterController != null, "Character controller is not set.");

            this.movement = this.GetComponent<IMove>();
            Debug.Assert(this.movement != null, "Movement is not set.");

            this.originalControllerHeight = this.characterController.height;
            this.originalHeadHeight = this.head.localPosition.y;

            this.originalWalkingSpeed = this.movement.WalkingSpeed;
            this.originalRunningSpeed = this.movement.RunningSpeed;
        }

        private void Update()
        {
            if (ControlsProvider.Actions.ToggleCrouch)
            {
                this.isCrouching = !this.isCrouching;

                if (this.isCrouching)
                {
                    this.SetControllerHeight(this.crouchHeight);
                    this.SetHeadHeight(this.originalHeadHeight - (this.originalControllerHeight - this.crouchHeight));

                    this.movement.WalkingSpeed = this.originalWalkingSpeed * this.crouchSpeedFactor;
                    this.movement.RunningSpeed = this.originalRunningSpeed * this.crouchSpeedFactor;
                }
                else
                {
                    this.SetControllerHeight(this.originalControllerHeight);
                    this.SetHeadHeight(this.originalHeadHeight);

                    this.movement.WalkingSpeed = this.originalWalkingSpeed;
                    this.movement.RunningSpeed = this.originalRunningSpeed;
                }
            }
        }

        private void SetControllerHeight(float height)
        {
            // TODO: make a smooth transition movement.
            this.characterController.height = height;
            this.characterController.center = new Vector3(
                this.characterController.center.x,
                height / 2,
                this.characterController.center.z);
        }

        private void SetHeadHeight(float height)
        {
            // TODO: make a smooth transition movement.
            this.head.localPosition = new Vector3(
                this.head.localPosition.x,
                height,
                this.head.localPosition.z);
        }
    }
}
