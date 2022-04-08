using GamePlay;
using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Attack/Player Attack Behaviour", fileName = "Player Attack Behaviour")]
public class PlayerAttackBehaviour : AttackBehaviour
{
    public PlayerController.FaceDirection faceDirectionPreference = PlayerController.FaceDirection.Right;
    bool isAttacking = false;
    public bool Attackable { get; set; }
    PlayerController player;
    public override void Initialize(GameObject owner, Transform weaponPos, params object[] args)
    {
        base.Initialize(owner, weaponPos, args);
        if (args.Length > 0 && args[0] is PlayerInput playerInput)
        {
            playerInput.actions["Fire"].performed += OnAttackPressed;
            playerInput.actions["Fire"].canceled += OnAttackReleased;
        }
        player = owner.GetComponent<PlayerController>();
        Attackable = true;
    }
    public override void Update()
    {
        if (isAttacking && Attackable)
        {
            base.Update();
            Vector3 mousePosToWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = new Vector2(mousePosToWorld.x - weaponPos.position.x, mousePosToWorld.y - weaponPos.position.y);
            faceDirectionPreference = direction.x < 0 ? PlayerController.FaceDirection.Left : PlayerController.FaceDirection.Right;
            weapon.Fire(weaponPos, direction);
        }
    }

    public void OnAttackPressed(InputAction.CallbackContext ctx)
    {
        isAttacking = true;
    }
    public void OnAttackReleased(InputAction.CallbackContext ctx)
    {
        isAttacking = false;
        faceDirectionPreference = PlayerController.FaceDirection.None;
    }
}
