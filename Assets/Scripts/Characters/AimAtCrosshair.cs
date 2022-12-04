using Assets.Scripts.Gunplay.Ballistics;
using Assets.Scripts.Gunplay.Guns;
using Assets.Scripts.Misc;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class AimAtCrosshair : MonoBehaviour, INotifyOnDied
    {
        [SerializeField]
        private Transform eyePoint;

        [SerializeField]
        private Transform gunHand;

        [SerializeField]
        private float angularSpeed = 30f;

        public void NotifyOnDied()
        {
            this.enabled = false;
        }

        private void Awake()
        {
            Debug.Assert(this.eyePoint != null);
            Debug.Assert(this.gunHand != null);
        }

        private void Update()
        {
            var aimDirection = this.DetermineAimDirection();
            var updatedLookDirection = TransformHelper.RotateTowardsAtSpeed(this.gunHand.forward, aimDirection, this.angularSpeed);

            this.gunHand.LookAt(this.gunHand.position + updatedLookDirection);
        }

        private Vector3 DetermineAimDirection()
        {
            var hits = Physics.RaycastAll(this.eyePoint.position, this.eyePoint.forward);

            foreach (var hit in hits.OrderBy(r => r.distance))
            {
                var target = hit.collider.GetComponentInParent<IHitTarget>();
                if (target == null)
                {
                    continue;
                }

                // Guns are filtered out, to prevent aiming at one's own gun.
                if (hit.collider.GetComponentInParent<IGun>() != null)
                {
                    continue;
                }

                // Aim at nearest target.
                var aimDirection = hit.point - this.gunHand.position;

                return aimDirection.normalized;
            }

            // Just aim staight ahead.
            return this.gunHand.parent.forward;
        }
    }
}