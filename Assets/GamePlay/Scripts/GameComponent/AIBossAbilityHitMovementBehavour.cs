using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;
using GamePlay.Weapons;
using System;

[CreateAssetMenu(menuName = "Movement/Boss Ability Hit Movement", fileName = "Boss Ability Hit Movement")]
public class AIBossAbilityHitMovementBehavour : MovementBehaviour
{
    [SerializeField]
    float charmingMoveSpeed;
    float charmingDuration;
    bool isCharming;
    float meleeAttackRange;

    [SerializeField]
    Weapon bossEnoughNearWeapon;
    [SerializeField]
    Weapon bossNotNearEnougWeapon;

    [SerializeField]
    System.Action onWeaponLaunched;
    Rigidbody2D ownerRb;
    Boss boss;
    public override void Initialize(GameObject owner, params object[] args)
    {
        base.Initialize(owner, args);
        ownerRb = owner.GetComponent<Rigidbody2D>();
        boss = owner.GetComponent<Boss>();
        meleeAttackRange = boss.meleeAttackRange;
        isCharming = true;
        charmingDuration = (float)args[0];
        GameCore.GameManager.Instance.StartCoroutine(CharmingCountdownCR());
    }

    public override void UpdateMovement()
    {
        if (isCharming)
        {
            PlayerController player = GameCore.GameManager.Instance.Player;
            Vector2 direction = player.transform.position - owner.transform.position;
            ownerRb.velocity = direction.normalized * charmingMoveSpeed;
            boss.DoWalkingAnimation();
        }
    }
    IEnumerator CharmingCountdownCR()
    {
        yield return new WaitForSeconds(charmingDuration);
        OnCharmingFinished();
    }
    void OnCharmingFinished()
    {
        isCharming = false;
        Vector2 direction = GameCore.GameManager.Instance.Player.transform.position - owner.transform.position;
        float distance = direction.magnitude;
        if (distance < meleeAttackRange)
        {
            Weapon meleeWeapon = Instantiate(bossEnoughNearWeapon);
            meleeWeapon.Initialize(owner);
            meleeWeapon.Fire(boss.meleeWeaponTransform, direction.normalized);
        }
        else
        {
            Weapon rangeWeapon = Instantiate(bossNotNearEnougWeapon);
            rangeWeapon.Initialize(owner);
            rangeWeapon.Fire(boss.meleeWeaponTransform, direction.normalized);
        }
        boss.DoMeleeAttackAnimation();
    }
}
