using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.Weapons;
using GamePlay;

[CreateAssetMenu(menuName = "Weapon/Boss Melee Weapon", fileName = "Boss Melee Weapon")]
public class BossMeleeWeapon : Weapon
{
    [SerializeField]
    float healthDmg = 1;
    [SerializeField]
    float clueDmg = 1;

    [SerializeField]
    float pushPlayerDistance;
    [SerializeField]
    float pushPlayerSpeed;

    bool fired = false;

    public override void Initialize(GameObject owner, params object[] args)
    {
        base.Initialize(owner, args);
        fired = false;
    }
    public override void Fire(Transform pos, Vector2 direction)
    {
        if (!fired)
        {
            Debug.Log("Boss Fired Melee Weapon!");
            base.Fire(pos, direction);
            Boss me = owner.GetComponent<Boss>();
            me.DoMeleeAttackAnimation();

            PlayerController player = GameCore.GameManager.Instance.Player;
            player.TakeDamage(healthDmg, me, IDamagable.DamageType.Health);
            player.TakeDamage(clueDmg, me, IDamagable.DamageType.Clue);

            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 distination = player.transform.position - me.transform.position;
            distination = playerPosition + distination.normalized * pushPlayerDistance;
            player.ForcePush(distination, pushPlayerSpeed);
            fired = true;
            me.SwitchToDefaultMoveBehaviour();
        }
    }
}

