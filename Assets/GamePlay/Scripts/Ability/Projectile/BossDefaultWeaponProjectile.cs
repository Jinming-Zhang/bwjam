using GamePlay;
using GamePlay.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossDefaultWeaponProjectile : Projectile
{
    [SerializeField]
    public float charmDuration;
    Boss boss;
    public override void Initiailze(GameObject src, params object[] args)
    {
        base.Initiailze(src, args);
        boss = src.GetComponent<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            boss.DefaultProjectileHitPlayer();
            player.TakeDamage(FinalDaage, this);
            Destroy(gameObject);
        }
    }
}
