using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character equips their gun.
    /// </summary>
    public class EquipGun : IBehaviour
    {
        private bool hasStartedEquipping = false;

        public string Description => "Equip the gun.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
            this.hasStartedEquipping = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            Debug.Assert(characterAccess.GunHandler.Gun != null);

            if (characterAccess.GunHandler.IsEquipped)
            {
                this.IsDone = true;
                return;
            }

            if (this.hasStartedEquipping)
            {
                return;
            }

            // Equip.
            characterAccess.GunHandler.Equip();
            this.hasStartedEquipping = true;
        }
    }
}
