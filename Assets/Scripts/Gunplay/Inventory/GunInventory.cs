using Assets.Scripts.Gunplay.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// See <see cref="IGunInventory{TGun}"/>.
    /// </summary>
    public class GunInventory : MonoBehaviour, IGunInventory<Gun>
    {
        private readonly IList<Gun> guns = new List<Gun>();

        [SerializeField]
        private Gun[] initialGunPrefabs;

        private int index = 0;

        public void AddGun(Gun gun)
        {
            Debug.Assert(gun != null);

            if (this.Contains(gun))
            {
                return;
            }

            this.guns.Add(gun);
        }

        public bool Contains(Gun gun)
        {
            return this.guns.Select(g => g.Properties.UniqueName).Contains(gun.Properties.UniqueName);
        }

        public Gun GetNextGun()
        {
            if (this.guns.Any())
            {
                this.index = (this.index + 1) % this.guns.Count;

                return this.guns[this.index];
            }
            else
            {
                return null;
            }
        }

        private void Awake()
        {
            if (this.initialGunPrefabs != null)
            {
                foreach (var gun in this.initialGunPrefabs)
                {
                    var gunInstance = Instantiate(gun);
                    GunHelper.HideGun(gunInstance);

                    this.AddGun(gunInstance);
                }
            }
        }
    }
}
