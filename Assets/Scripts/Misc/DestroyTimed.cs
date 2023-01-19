
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class DestroyTimed : MonoBehaviour
    {
        private IEnumerable<Renderer> renderersOptional;

        [SerializeField]
        private float lifetime = 10f;

        private void Awake()
        {
            this.renderersOptional = this.GetComponentsInChildren<Renderer>();
            this.StartCoroutine(this.DestroyAfterSeconds(this.lifetime));
        }

        private IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            while (this.renderersOptional.Any(r => r.isVisible))
            {
                yield return new WaitForSeconds(1f);
            }

            Destroy(this.gameObject);
        }
    }
}