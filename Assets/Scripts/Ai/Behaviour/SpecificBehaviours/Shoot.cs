
namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character shots their currently equipped gun.
    /// </summary>
    public class Shoot : IBehaviour
    {
        private bool pulledTrigger = false;

        public string Description => "Shoot the gun.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess, float deltaTime)
        {
            if (!characterAccess.GunHandler.IsEquipped)
            {
                this.IsDone = true;
                return;
            }

            if (this.pulledTrigger)
            {
                characterAccess.GunHandler.Shoot();
            }

            this.pulledTrigger = !this.pulledTrigger;
        }
    }
}