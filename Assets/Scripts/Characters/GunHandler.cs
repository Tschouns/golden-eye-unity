using Assets.Scripts.Gunplay.Guns;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    /// <summary>
    /// Implements <see cref="GunHandler"/>.
    /// </summary>
    public class GunHandler : MonoBehaviour, IGunHandler, INotifyOnDied
    {
        [SerializeField]
        private Gun gun;

        [SerializeField]
        private Transform gunHand;

        [SerializeField]
        private Transform gunHolster;

        [SerializeField]
        private bool startEquipped = false;

        private bool isDropped = false;

        public IGun Gun => this.gun;

        public bool IsEquipped { get; private set; }

        public void Equip()
        {
            if (this.isDropped)
            {
                return;
            }

            this.gun.transform.parent = this.gunHand;
            this.gun.transform.localPosition = Vector3.zero;
            this.gun.transform.localRotation = Quaternion.identity;
            this.IsEquipped = true;
        }

        public void Unequip()
        {
            if (this.isDropped)
            {
                return;
            }

            this.gun.transform.parent = this.gunHolster;
            this.gun.transform.localPosition = Vector3.zero;
            this.gun.transform.localRotation = Quaternion.identity;
            this.IsEquipped = false;
        }

        public void Drop()
        {
            this.gun.transform.parent = null;
            this.IsEquipped = false;
            this.isDropped = true;

            this.gun.ActivatePhysics();
        }

        public void Shoot()
        {
            if (this.isDropped)
            {
                return;
            }

            if (this.IsEquipped)
            {
                this.Gun.Trigger();
            }
        }

        public void NotifyOnDied()
        {
            this.Drop();
        }

        private void Awake()
        {
            Debug.Assert(this.gun != null);
            Debug.Assert(this.gunHand != null);
            Debug.Assert(this.gunHolster != null);

            if (this.startEquipped)
            {
                this.Equip();
            }
            else
            {
                this.Unequip();
            }
        }
    }
}