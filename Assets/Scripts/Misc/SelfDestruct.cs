using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Causes a game object to self-destruct after a specified time-to-live.
    /// </summary>
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField]
        private float selfDestructAfterSeconds = 1f;

        private void Awake()
        {
            var delay = Mathf.Max(selfDestructAfterSeconds, 0.000001f);
            this.StartCoroutine(this.SelfDestructAfterSeconds(delay));
        }

        private IEnumerator SelfDestructAfterSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(this.gameObject);
            yield return null;
        }
    }
}
