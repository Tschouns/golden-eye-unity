using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class RotateToTarget : MonoBehaviour
    {
        [SerializeField]
        private float angularSpeed = 180;

        private float startRotationX;
        private float targetRotationOffsetX = 0f;
        private float currentRotationCurrentX = 0f;

        public void SetRotationTarget(float rotationDeg)
        {
            this.targetRotationOffsetX = rotationDeg;
        }

        private void Awake()
        {
            this.startRotationX = this.transform.localRotation.eulerAngles.x;
        }

        private void Update()
        {
            this.UpdateCurrentRotation();
            this.ApplyCurrentRotation();
        }

        private void UpdateCurrentRotation()
        {
            var targetDelta = this.targetRotationOffsetX - this.currentRotationCurrentX;
            var delta = this.angularSpeed * Time.deltaTime;

            if (targetDelta > 0)
            {
                delta = Mathf.Min(delta, targetDelta);
            }
            else
            {
                delta = Mathf.Max(-delta, targetDelta);
            }

            this.currentRotationCurrentX += delta;
        }

        private void ApplyCurrentRotation()
        {
            var totalRotation = this.startRotationX + this.currentRotationCurrentX;

            this.transform.localRotation = Quaternion.Euler(
                totalRotation,
                this.transform.localRotation.y,
                this.transform.localRotation.z);
        }
    }
}