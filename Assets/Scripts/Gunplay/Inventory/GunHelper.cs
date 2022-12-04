using Assets.Scripts.Gunplay.Guns;
using UnityEngine;

namespace Assets.Scripts.Gunplay.Inventory
{
    /// <summary>
    /// Provides methods for handling guns.
    /// </summary>
    public static class GunHelper
    {
        private static readonly Vector3 positionFarFarAway = new Vector3(0, -1000, 0);

        /// <summary>
        /// Hides the specified gun in Nirvana.
        /// </summary>
        /// <param name="gun">
        /// The gun to hide
        /// </param>
        public static void HideGun(Gun gun)
        {
            Debug.Assert(gun != null);

            gun.transform.parent = null;
            gun.transform.position = positionFarFarAway;
            gun.transform.rotation = Quaternion.identity;

            gun.DeactivatePhysics();
        }

        /// <summary>
        /// Produces the specified gun, i.e. put it in the specified gun slot.
        /// </summary>
        /// <param name="gun">
        /// The gun to produce
        /// </param>
        /// <param name="gunSlot">
        /// The gun slot to put the gun in
        /// </param>
        public static void ProduceGun(Gun gun, Transform gunSlot)
        {
            Debug.Assert(gun != null);
            Debug.Assert(gunSlot != null);

            gun.transform.parent = gunSlot;
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;

            gun.DeactivatePhysics();
        }
    }
}
