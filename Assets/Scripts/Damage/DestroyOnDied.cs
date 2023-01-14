using UnityEngine;

namespace Assets.Scripts.Damage
{
    /// <summary>
    /// Destroys the attaching game object when the parent with health dies.
    /// </summary>
    public class DestroyOnDied : MonoBehaviour, INotifyOnDied
    {
        public void NotifyOnDied()
        {
            Destroy(this.gameObject);
        }
    }
}
