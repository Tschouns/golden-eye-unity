using Assets.Scripts.Gunplay.Ballistics;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Effects
{
    /// <summary>
    /// Activates an object's rigidbody physics when it's hit.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ActivatePhysicsHitEffect : MonoBehaviour, IHitEffect
    {
        private Rigidbody myRigidbody;
        private bool isActivated = false;

        [SerializeField]
        private bool includeDeflected = false;

        public void ReactToImpact(BulletImpact impact)
        {
            if (this.isActivated)
            {
                return;
            }

            if (this.includeDeflected ||
                impact.Type is BulletImpactType.Penetrated or BulletImpactType.Pierced)
            {
                this.myRigidbody.isKinematic = false;
                this.isActivated = true;
            }
        }

        private void Awake()
        {
            this.myRigidbody = this.GetComponent<Rigidbody>();
            Debug.Assert(this.myRigidbody != null);
        }
    }
}
