using GamePlay;
using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BossDefaultMovement : MovementBehaviour
{
    [Header("Movement")]
    [SerializeField]
    protected float initialSpeed;
    [HideInInspector]
    public float ingameMovingSpeed;

    [Header("Attack")]
    protected float defaultAttackRange;
    [SerializeField]
    protected Weapon defaultWeaponTemplate;
    protected Weapon weapon;
    PlayerController player => GameCore.GameManager.Instance.Player;

    Boss boss;
    Rigidbody2D rb;
    Transform defaultWeaponTransform;

    public void Setup(Boss boss, Rigidbody2D rb, Transform defaultWeaponTransform, float defaultAttackRange)
    {
        ingameMovingSpeed = initialSpeed;
        this.defaultAttackRange = defaultAttackRange;
        this.boss = boss;
        this.rb = rb;
        this.defaultWeaponTransform = defaultWeaponTransform;
        weapon = Instantiate(defaultWeaponTemplate);
        weapon.Initialize(owner);
    }

    public override void Initialize(GameObject owner, params object[] args)
    {
        base.Initialize(owner, args);
    }

    public override void UpdateMovement()
    {
        if (player)
        {
            Vector2 tarDirection = player.transform.position - defaultWeaponTransform.position;
            // out of range, move to player
            if (tarDirection.magnitude > defaultAttackRange)
            {
                rb.velocity = tarDirection.normalized * ingameMovingSpeed;
                boss.DoWalkingAnimation();
            }
            // attack player
            else
            {
                rb.velocity = Vector2.zero;
                boss.DoKissAnimation();
                weapon.Fire(defaultWeaponTransform, tarDirection);
            }
        }

    }
}
