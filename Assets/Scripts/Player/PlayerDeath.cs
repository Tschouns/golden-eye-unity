using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Disables all specified player control inputs when the player character dies.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private Transform playerModel;

        private CharacterController characterController;

        private void Awake()
        {
            Debug.Assert(this.health != null, "Health is not set.");
            Debug.Assert(this.playerModel != null, "Player model is not set.");

            this.characterController = this.GetComponent<CharacterController>();

            Debug.Assert(this.characterController != null, "Character controller is not set.");

            this.health.Died += this.OnDied;
        }

        private void OnDied()
        {
            Debug.Log("Player died!");

            this.characterController.enabled = false;

            // "Unpack" player model.
            this.playerModel.parent = this.transform.parent;
        }
    }
}