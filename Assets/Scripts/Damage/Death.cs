using UnityEngine;

namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Implements death and dying, when a characters health runs out.
    /// Notifies all child components which implement <see cref="INotifyOnDied"/>.
    /// </summary>
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private Rigidbody myRigidbody;

        private INotifyOnDied[] componentsToNotify;

        private void Awake()
        {
            Debug.Assert(this.health != null, "Health is not set.");
            Debug.Assert(this.myRigidbody != null, "Rigidbody is not set.");

            this.componentsToNotify = this.GetComponentsInChildren<INotifyOnDied>();

            this.health.Died += this.OnDied;
        }

        private void OnDied()
        {
            // Activate rigidbody physics.
            this.myRigidbody.isKinematic = false;
            this.myRigidbody.useGravity = true;

            foreach (var component in this.componentsToNotify)
            {
                component.NotifyOnDied();
            }
        }
    }
}
