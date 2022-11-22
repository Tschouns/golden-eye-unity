using UnityEngine;

namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Disables the object's renderer on Awake.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class DisableRenderer : MonoBehaviour
    {
        private void Awake()
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.enabled = false;
        }
    }
}
