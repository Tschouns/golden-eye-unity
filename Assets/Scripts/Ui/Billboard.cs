using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class Billboard : MonoBehaviour
    {
        private Transform cameraTransform;

        private void Awake()
        {
            this.cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            this.transform.LookAt(
                this.transform.position + this.cameraTransform.rotation * Vector3.forward,
                this.cameraTransform.rotation * Vector3.up
            );
        }
    }
}
