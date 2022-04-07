using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        float lifeTime = 6f;
        [SerializeField]
        float baseDmg = 1f;

        public float BaseDmg { get => baseDmg; set => baseDmg = value; }

        [SerializeField]
        float velocity = 1f;
        public float Velocity { get => velocity; set => velocity = value; }

        [SerializeField]
        float critHitProbability = .1f;
        public float CritHitProbabilityMultiplier { get => critHitProbability; set => critHitProbability = value; }

        [SerializeField]
        float critHitDmgMultiplier = 1f;
        public float CritHitDmgMultiplier { get => critHitDmgMultiplier; set => critHitDmgMultiplier = value; }


        public virtual void Launch(Vector2 direction)
        {
            GetComponent<Rigidbody2D>().velocity = direction.normalized * Velocity;
            StartCoroutine(SelfDestroyCR());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            if (target != null)
            {
                float dmg = Random.Range(0f, 1f) <= critHitProbability ? BaseDmg * critHitDmgMultiplier : BaseDmg;
                target.TakeDamage(dmg, this);
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }

        IEnumerator SelfDestroyCR()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
