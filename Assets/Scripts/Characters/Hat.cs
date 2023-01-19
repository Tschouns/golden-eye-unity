using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Implements a hat, which falls off when a character dies.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Hat : MonoBehaviour, INotifyOnDied
    {
        private Rigidbody myRigidbody;

        public void NotifyOnDied()
        {
            this.myRigidbody.isKinematic = false;
        }

        private void Awake()
        {
            this.myRigidbody = this.GetComponent<Rigidbody>();
            Debug.Assert(this.myRigidbody != null);
        }
    }
}
