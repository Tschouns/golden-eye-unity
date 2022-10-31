using Assets.Scripts.Gunplay.Ballistics;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Applies force to a rigidbody when hit.
    /// </summary>
    [RequireComponent(typeof(HitTarget))]
    public class ForceHitEffect : MonoBehaviour, IHitEffect
    {
        private Rigidbody myRigidbody;

        public void ReactToImpact(BulletImpact impact)
        {
            this.myRigidbody.AddForceAtPosition(
                impact.GetImpulse(),
                impact.EntryPoint,
                ForceMode.Impulse);
        }

        private void Awake()
        {
            this.myRigidbody = this.GetComponentInParent<Rigidbody>();
            Debug.Assert(this.myRigidbody != null);
        }
    }
}
