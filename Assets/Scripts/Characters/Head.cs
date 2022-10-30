using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Marks a character's head.
    /// </summary>
    public class Head : MonoBehaviour, IHead
    {
        [SerializeField]
        private Transform eyePosition;

        public Vector3 Position => this.transform.position;
        public Vector3 LookDirection => this.transform.forward;
        public Vector3 EyePosition => this.eyePosition.position;

        private void Awake()
        {
            Debug.Assert(this.eyePosition != null);
        }
    }
}
