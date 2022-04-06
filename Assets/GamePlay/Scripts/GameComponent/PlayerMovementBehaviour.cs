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
        float ingameMoveSpeed;
        Vector2 movement;
        Rigidbody2D rb;
        public override void Initialize(GameObject owner, object args)
        {
            base.Initialize(owner, args);
            if (args is PlayerInput playerInput)
            {
                playerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
                playerInput.SwitchCurrentActionMap("Player");
                playerInput.actions["Move"].performed += OnPlayerMovePerformed;
                playerInput.actions["Move"].canceled += OnPlayerMoveCancelled;
            }
            rb = owner.GetComponent<Rigidbody2D>();
            ingameMoveSpeed = initialMoveSpeed;
        }

        public override void UpdateMovement()
        {
            Vector2 delta = ingameMoveSpeed * movement * Time.deltaTime;
            Vector3 tarPos = owner.transform.position + new Vector3(delta.x, delta.y, 0);
            rb.velocity = ingameMoveSpeed * movement;
            //rb.MovePosition(tarPos);
        }


        public void OnPlayerMovePerformed(CallbackContext ctx)
        {
            movement = ctx.ReadValue<Vector2>();
        }
        public void OnPlayerMoveCancelled(CallbackContext ctx)
        {
            movement = Vector2.zero;
        }
    }
}