using Assets.Scripts.Controls;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Reacts to player input and controls player movement.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonMovement : MonoBehaviour, IMove
    {
        [SerializeField]
        private float walkingSpeed = 3f;

        [SerializeField]
        private float runningSpeed = 5f;

        private CharacterController characterController;

        public float WalkingSpeed
        {
            get => this.walkingSpeed;
            set => this.walkingSpeed = value;
        }

        public float RunningSpeed
        {
            get => this.runningSpeed;
            set => this.runningSpeed = value;
        }

        private void Awake()
        {
            this.characterController = this.GetComponent<CharacterController>();
            Debug.Assert(this.characterController != null);
        }

        private void Update()
        {
            var direction = Vector3.zero;

            if (ControlsProvider.Actions.Forward)
            {
                direction += this.transform.forward;
            }

            if (ControlsProvider.Actions.Backward)
            {
                direction -= this.transform.forward;
            }

            if (ControlsProvider.Actions.Left)
            {
                direction -= this.transform.right;
            }

            if (ControlsProvider.Actions.Right)
            {
                direction += this.transform.right;
            }

            var speed = ControlsProvider.Actions.Run ? this.runningSpeed : this.walkingSpeed;
            this.characterController.SimpleMove(direction * speed);
        }
    }
}