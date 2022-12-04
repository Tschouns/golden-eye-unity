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
        private Gun[] initialGuns;

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
            return this.guns.Select(g => g.UniqueName).Contains(gun.UniqueName);
        }

        public Gun GetNextGun()
        {
            if (this.guns.Any())
            {
                return this.guns[this.index++ % this.guns.Count];
            }
            else
            {
                return null;
            }
        }

        private void Awake()
        {
            if (this.initialGuns != null)
            {
                foreach (var gun in this.initialGuns)
                {
                    GunHelper.HideGun(gun);
                    this.AddGun(gun);
                }
            }
        }
    }
}
