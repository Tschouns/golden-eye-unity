
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    [RequireComponent(typeof(Rigidbody))]
    public class DestroyTimed : MonoBehaviour
    {
        [SerializeField]
        private float physicsLifetime = 10f;

        private void Awake()
        {
            this.StartCoroutine(this.DestroyPhysicsAfterSeconds(this.physicsLifetime));
        }

        private IEnumerator DestroyPhysicsAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(this);
        }
    }
}