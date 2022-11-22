
namespace Assets.Scripts.Ai.Behaviour.SpecificBehaviours
{
    /// <summary>
    /// A character shots their currently equipped gun.
    /// </summary>
    public class Shoot : IBehaviour
    {
        public string Description => "Shoot a gun.";

        public bool IsDone { get; private set; } = false;

        public void Reset()
        {
            this.IsDone = false;
        }

        public void Update(ICharacterAccess characterAccess)
        {
            if (!characterAccess.GunHandler.IsEquipped)
            {
                this.IsDone = true;
                return;
            }

            characterAccess.GunHandler.Shoot();
        }
    }
}