using System.Collections;
using UnityEngine;

namespace GamePlay.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected float lifeTime = 6f;
        [SerializeField]
        protected float baseDmg = 1f;

        public float BaseDmg { get => baseDmg; set => baseDmg = value; }

        [SerializeField]
        protected float velocity = 1f;
        public float Velocity { get => velocity; set => velocity = value; }

        [SerializeField]
        protected float critHitProbability = .1f;
        public float CritHitProbabilityMultiplier { get => critHitProbability; set => critHitProbability = value; }

        [SerializeField]
        protected float critHitDmgMultiplier = 1f;
        public float CritHitDmgMultiplier { get => critHitDmgMultiplier; set => critHitDmgMultiplier = value; }

        protected float FinalDaage => Random.Range(0f, 1f) <= critHitProbability ? BaseDmg * critHitDmgMultiplier : BaseDmg;

        public virtual void Initiailze(GameObject src, params object[] args) { }
        public virtual void Launch(Vector2 direction)
        {
            GetComponent<Rigidbody2D>().velocity = direction.normalized * Velocity;
            StartCoroutine(SelfDestroyCR());
        }


        IEnumerator SelfDestroyCR()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
