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
            Debug.Assert(health != null);
            Debug.Assert(this.myRigidbody != null);

            this.componentsToNotify = this.GetComponentsInChildren<INotifyOnDied>();

            health.Died += this.OnDied;
        }

        private void OnDied()
        {
            Debug.Log("Has died!");

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