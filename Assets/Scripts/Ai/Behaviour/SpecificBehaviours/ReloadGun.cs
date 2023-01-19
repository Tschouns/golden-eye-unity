
using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character reloads their currently equipped gun.
    /// </summary>
    public class ReloadGun : IBehaviour
    {
        private bool hasStartedReloading = false;

        public string Description => "Reload the gun.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
            this.hasStartedReloading = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            Debug.Assert(characterAccess.GunHandler.Gun != null);

            var gun = characterAccess.GunHandler.Gun;

            if (gun.CurrentNumberOfBullets == gun.Properties.ClipSize)
            {
                this.IsDone = true;
                return;
            }

            if (this.hasStartedReloading)
            {
                return;
            }

            // Reload -- from an inventory of "unlimited" bullets.
            characterAccess.GunHandler.Reload(int.MaxValue);
            this.hasStartedReloading = true;
        }
    }
}
