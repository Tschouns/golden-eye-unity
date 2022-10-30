using Assets.Scripts.Damage;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Implements death and dying, when a characters health runs out.
    /// </summary>
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private Rigidbody myRigidbody;

        private void Awake()
        {
            Debug.Assert(health != null);
            Debug.Assert(this.myRigidbody != null);

            health.Died += this.OnDied;
        }

        private void OnDied()
        {
            this.myRigidbody.isKinematic = false;
            this.myRigidbody.useGravity = true;
            Debug.Log("Has died!");
        }
    }
}