using UnityEngine;

namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character unequips their gun.
    /// </summary>
    public class UnequipGun : IBehaviour
    {
        private bool hasStartedUnequipping = false;

        public string Description => "Unequip the gun.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
            this.hasStartedUnequipping = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            Debug.Assert(characterAccess.GunHandler.Gun != null);

            if (!characterAccess.GunHandler.IsEquipped)
            {
                this.IsDone = true;
                return;
            }

            if (this.hasStartedUnequipping)
            {
                return;
            }

            // Equip.
            characterAccess.GunHandler.Unequip();
            this.hasStartedUnequipping = true;
        }
    }
}
