using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Movement/PlayerMovement", fileName = "Player Movement Behaviour")]
    public class PlayerMovementBehaviour : MovementBehaviour
    {
        [SerializeField]
        float initialMoveSpeed;
        [SerializeField]
        float dodgingSpeedMultiplier = 1.2f;
        [SerializeField]
        float dodgingDistance;

        float ingameMoveSpeed;
        Vector2 movement;
        Rigidbody2D rb;

        bool isDodging;
        Vector2 dodgingVelocity;
        float dodgingTimer = 0;
        public override void Initialize(GameObject owner, params object[] args)
        {
            base.Initialize(owner, args);
            if (args[0] is PlayerInput playerInput)
            {
                playerInput.actions["Move"].performed += OnPlayerMovePerformed;
                playerInput.actions["Move"].canceled += OnPlayerMoveCancelled;
                playerInput.actions["Dodge"].canceled += OnPlayerDodgePerformed;
            }
            rb = owner.GetComponent<Rigidbody2D>();
            ingameMoveSpeed = initialMoveSpeed;
        }

        public override void UpdateMovement()
        {
            if (!isDodging)
            {
                rb.velocity = ingameMoveSpeed * movement;
            }
            else
            {
                rb.velocity = dodgingVelocity;
                dodgingTimer -= Time.fixedDeltaTime;
                isDodging = dodgingTimer > 0;
            }
        }


        public void OnPlayerMovePerformed(CallbackContext ctx)
        {
            movement = ctx.ReadValue<Vector2>();
        }
        public void OnPlayerMoveCancelled(CallbackContext ctx)
        {
            movement = Vector2.zero;
        }

        void OnPlayerDodgePerformed(CallbackContext ctx)
        {
            if (movement.magnitude > 0)
            {
                dodgingVelocity = movement.normalized * ingameMoveSpeed * dodgingSpeedMultiplier;
                dodgingTimer = dodgingDistance / (dodgingVelocity.magnitude);
                isDodging = true;
            }
        }
    }
}