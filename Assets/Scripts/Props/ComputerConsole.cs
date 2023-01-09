using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Props
{
    /// <summary>
    /// A destructable computer console.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class ComputerConsole : MonoBehaviour
    {
        [SerializeField]
        private KeyBoard keyboard;

        private void Awake()
        {
            Debug.Assert(this.keyboard != null);

            var health = this.GetComponent<IHealth>();
            Debug.Assert(health != null);

            health.Died += () => this.keyboard.IsOn = false;
        }
    }
}
