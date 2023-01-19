
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class DestroyTimed : MonoBehaviour
    {
        [SerializeField]
        private float lifetime = 10f;

        private void Awake()
        {
            this.StartCoroutine(this.DestroyAfterSeconds(this.lifetime));
        }

        private IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(this);
        }
    }
}