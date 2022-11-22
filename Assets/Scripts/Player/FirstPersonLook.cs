using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Reacts to player look axes input and controls player look direction -- i.e. rotates the player and camera.
    /// </summary>
    public class FirstPersonLook : PlayerInputBase
    {
        [SerializeField]
        private Transform playerCharacter;

        [SerializeField]
        private Transform firstPersonCamera;

        [SerializeField]
        private float mouseSensitivity = 1f;

        private float cameraRotationX = 0f;

        private void Awake()
        {
            Debug.Assert(this.playerCharacter != null);
            Debug.Assert(this.firstPersonCamera != null);

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            var playerRrotationY = mouseX * this.mouseSensitivity * Time.deltaTime;

            this.cameraRotationX += -mouseY * this.mouseSensitivity * Time.deltaTime;
            this.cameraRotationX = Mathf.Clamp(this.cameraRotationX, -90f, 90f);

            this.playerCharacter.Rotate(this.playerCharacter.up, playerRrotationY);
            this.firstPersonCamera.localRotation = Quaternion.Euler(this.cameraRotationX, 0, 0);
        }
    }
}
