using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Projectiles
{
    public class BasicEnemyProjectile : Projectile
    {
        [SerializeField]
        float clueDamage = 0;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            if (target != null)
            {
                float dmg = Random.Range(0f, 1f) <= critHitProbability ? BaseDmg * critHitDmgMultiplier : BaseDmg;
                target.TakeDamage(dmg, this, IDamagable.DamageType.Health);
                target.TakeDamage(clueDamage, this, IDamagable.DamageType.Clue);
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }
    }
}
