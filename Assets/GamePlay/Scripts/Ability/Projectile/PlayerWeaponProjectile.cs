using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Projectiles
{
    public class PlayerWeaponProjectile : Projectile
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(FinalDaage, this);
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }
    }
}
