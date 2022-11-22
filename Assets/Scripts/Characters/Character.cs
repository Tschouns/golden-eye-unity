
using Assets.Scripts.Damage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Marks a character as a characeter. Provides basic information on the character.
    /// </summary>
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private Head head;

        [SerializeField]
        private Transform[] visibleParts;

        [SerializeField]
        private Health health;

        [SerializeField]
        private string faction = "soviet";

        public IHead Head => this.head;
        public IEnumerable<Vector3> VisiblePoints { get; private set; }
        public string Faction => this.faction;
        public Vector3 Position => this.transform.position;
        public bool IsAlive { get; private set; } = true;

        private void Awake()
        {
            Debug.Assert(this.head != null);
            Debug.Assert(this.visibleParts != null);

            this.VisiblePoints = this.visibleParts
                .Select(t => t.position)
                .ToArray();

            if (this.health != null)
            {
                this.health.Died += () => this.IsAlive = false;
            }
        }
    }
}
