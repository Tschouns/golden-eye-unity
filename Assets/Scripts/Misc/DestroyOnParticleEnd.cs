using UnityEngine;

namespace Assets.Scripts.Misc
{
    /// <summary>
    /// Destroys the spawned Gameobject, after the ParticleSystem has expired.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class DestroyOnParticleEnd : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            Destroy(this.gameObject);
        }
    }
}
