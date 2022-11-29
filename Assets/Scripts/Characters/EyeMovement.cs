using Assets.Scripts.Misc;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Simulates realistic eye movement -- aestetic/juicyness purposes.
    /// </summary>
    public class EyeMovement : MonoBehaviour, IEyes, INotifyOnDied
    {
        [SerializeField]
        private Head head;

        [SerializeField]
        private Transform leftEye;

        [SerializeField]
        private Transform rightEye;

        [SerializeField]
        private float maxAngle = 50f;

        [SerializeField]
        private float seccadeAngle = 15f;

        private Vector3? explicitFocusPoint;
        private Vector3 currentFocusPoint;

        public void SetEyesFocus(Vector3 focusPoint)
        {
            this.explicitFocusPoint = focusPoint;
        }

        public void UnsetEyesFocus()
        {
            this.explicitFocusPoint = null;
        }

        public void NotifyOnDied()
        {
            this.enabled = false;
        }

        private void Awake()
        {
            Debug.Assert(this.head != null);
            Debug.Assert(this.leftEye != null);
            Debug.Assert(this.rightEye != null);

            this.StopCoroutine(this.PeriodicRefocus());
        }

        private void Update()
        {
            var focusAngle = this.GetCurrentFocusPointAngle();

            if (this.explicitFocusPoint == null)
            {
                // Simulate seccade movement.
                if (focusAngle > this.seccadeAngle)
                {
                    this.FindNewFocusPoint();
                }
            }
            else
            {
                this.currentFocusPoint = this.explicitFocusPoint.Value;

                // If the explicit focus point is out of reach, lose it (for next iteration).
                if (focusAngle > this.maxAngle)
                {
                    this.explicitFocusPoint = null;
                }
            }

            // Move the eyes.
            this.TurnEye(this.leftEye);
            this.TurnEye(this.rightEye);
        }

        private float GetCurrentFocusPointAngle()
        {
            var focusPointAngle = Vector3.Angle(
                this.currentFocusPoint - this.head.Position,
                this.head.transform.forward);

            return focusPointAngle;
        }

        private void FindNewFocusPoint()
        {
            this.currentFocusPoint = this.head.Position + (this.head.transform.forward * 10);
        }

        private void TurnEye(Transform eye)
        {
            var focusDirection = this.currentFocusPoint - eye.position;
            var updatedEyeDirection = TransformHelper.RotateTowardsAtSpeed(eye.forward, focusDirection, 300);

            eye.LookAt(eye.position + updatedEyeDirection);
        }

        private IEnumerator PeriodicRefocus()
        {
            if (this.explicitFocusPoint == null)
            {
                this.FindNewFocusPoint();
            }

            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }
}
