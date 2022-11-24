﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Continuously evaluates whether the specified "gound sensor" is currently whithin a certain "max distance" of the ground.
    /// </summary>
    public class GroundCheck : MonoBehaviour, IGroundCheck
    {
        [SerializeField]
        private Transform groundSensor;

        [SerializeField]
        private LayerMask groundLayerMask;

        [SerializeField]
        private float maxDistance = 0.05f;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            this.IsGrounded = Physics.CheckSphere(this.groundSensor.position, this.maxDistance, this.groundLayerMask);
        }
    }
}