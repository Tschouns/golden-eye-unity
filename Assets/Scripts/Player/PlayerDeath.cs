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

        private void Awake()
        {
            Debug.Assert(this.health != null);
            Debug.Assert(this.playerModel != null);

            this.characterController = this.GetComponent<CharacterController>();

            Debug.Assert(this.characterController != null);

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