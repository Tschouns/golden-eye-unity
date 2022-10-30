using Assets.Scripts.Damage;
using System;
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
        private PlayerInputBase[] playerInputBehaviours;

        private void Awake()
        {
            Debug.Assert(this.health != null);
            Debug.Assert(this.playerModel != null);

            this.characterController = this.GetComponent<CharacterController>();
            this.playerInputBehaviours = this.GetComponents<PlayerInputBase>();

            Debug.Assert(this.characterController != null);
            Debug.Assert(this.playerInputBehaviours != null);

            this.health.Died += this.OnDied;
        }

        private void OnDied()
        {
            Debug.Log("Player died!");

            this.characterController.enabled = false;

            foreach (var inputBehaviour in this.playerInputBehaviours)
            {
                inputBehaviour.enabled = false;
            }

            // "Unpack" player model.
            this.playerModel.parent = this.transform.parent;
        }
    }
}