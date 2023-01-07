﻿using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Reacts to player look axes input and controls player look direction -- i.e. rotates the player and camera.
    /// </summary>
    public class FirstPersonLook : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private Transform playerCharacter;

        [SerializeField]
        private Transform firstPersonCamera;

        [SerializeField]
        private float mouseSensitivity = 1f;

        private float cameraRotationX = 0f;

        public void NotifyOnDied()
        {
            this.enabled = false;
        }

        private void Awake()
        {
            Debug.Assert(this.playerCharacter != null, "Player character is not set.");
            Debug.Assert(this.firstPersonCamera != null, "First person camera is not set.");

            Cursor.lockState = CursorLockMode.Locked;
            // Hint: Because the webGL Build has a completely different Behaviour, we scale the mouse sensitivity up for the editor.
            // This is not a perfect solution, because it creates a different output for different Environments, but it works for now.
#if UNITY_EDITOR
            this.mouseSensitivity *= 20;
#endif
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float playerRrotationY = mouseX * this.mouseSensitivity * Time.deltaTime;

            this.cameraRotationX += -mouseY * this.mouseSensitivity * Time.deltaTime;
            this.cameraRotationX = Mathf.Clamp(this.cameraRotationX, -90f, 90f);

            this.playerCharacter.Rotate(this.playerCharacter.up, playerRrotationY);
            this.firstPersonCamera.localRotation = Quaternion.Euler(this.cameraRotationX, 0, 0);
        }
    }
}
