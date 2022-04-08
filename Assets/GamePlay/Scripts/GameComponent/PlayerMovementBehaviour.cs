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
        public float speedMultiplier = 1f;
        Vector2 movement;
        Vector2 previousMovement;
        Rigidbody2D rb;

        bool isDodging;
        Vector2 dodgingVelocity;
        float dodgingTimer = 0;
        PlayerController player;
        public PlayerController.FaceDirection faceDirectionPreference = PlayerController.FaceDirection.Right;

        public override void Initialize(GameObject owner, params object[] args)
        {
            base.Initialize(owner, args);
            if (args[0] is PlayerInput playerInput)
            {
                playerInput.actions["Move"].performed += OnPlayerMovePerformed;
                playerInput.actions["Move"].canceled += OnPlayerMoveCancelled;
                playerInput.actions["Dodge"].canceled += OnPlayerDodgePerformed;
            }
            player = owner.GetComponent<PlayerController>();
            rb = owner.GetComponent<Rigidbody2D>();
            ingameMoveSpeed = initialMoveSpeed;
        }

        public override void UpdateMovement()
        {
            if (!isDodging)
            {
                rb.velocity = ingameMoveSpeed * movement * speedMultiplier;
                if (movement.magnitude > 0)
                {
                    UpdateMovingAnimation();
                }
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
            previousMovement = movement;
            movement = ctx.ReadValue<Vector2>();
        }
        public void OnPlayerMoveCancelled(CallbackContext ctx)
        {
            previousMovement = movement;
            movement = Vector2.zero;
            UpdateStableAnimation();
        }


        void UpdateMovingAnimation()
        {
            if (movement.x != 0)
            {
                player.Animator.Play(AnimationConstants.Player_Walk_side);
                faceDirectionPreference = movement.x < 0 ? PlayerController.FaceDirection.Left : PlayerController.FaceDirection.Right;
            }
            else if (movement.y != 0)
            {
                if (movement.y > 0)
                {
                    player.Animator.Play(AnimationConstants.Player_Walk_back);
                }
                else
                {
                    player.Animator.Play(AnimationConstants.Player_Walk_front);
                }
            }
        }
        void UpdateStableAnimation()
        {
            if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationConstants.Player_Walk_back))
            {
                player.Animator.Play(AnimationConstants.Player_Idle_Back);
            }
            else if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationConstants.Player_Walk_front))
            {
                player.Animator.Play(AnimationConstants.Player_Idle_front);
            }
            else if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationConstants.Player_Walk_side))
            {
                player.Animator.Play(AnimationConstants.Player_Idle_side);
            }
            else
            {
                player.Animator.Play(AnimationConstants.Player_Idle_front);
            }
        }
        void OnPlayerDodgePerformed(CallbackContext ctx)
        {
            if (movement.magnitude > 0)
            {
                dodgingVelocity = movement.normalized * ingameMoveSpeed * speedMultiplier * dodgingSpeedMultiplier;
                dodgingTimer = dodgingDistance / (dodgingVelocity.magnitude);
                isDodging = true;
            }
        }
    }
}