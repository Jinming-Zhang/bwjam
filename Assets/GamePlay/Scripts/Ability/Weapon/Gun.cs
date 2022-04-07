using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.Projectiles;

namespace GamePlay.Weapons
{
    [CreateAssetMenu(menuName = "Weapon/Gun", fileName = "Gun")]
    public class Gun : Weapon
    {
        [SerializeField]
        protected float bulletPerSec;
        [SerializeField]
        protected int clipSize;
        [SerializeField]
        protected float reloadTime;
        [SerializeField]
        protected Projectile projectileTemplate;

        [Header("Ammo Splitting")]
        [SerializeField]
        int splitAmount = 1;
        [SerializeField]
        float splitAngle = 10f;
        [SerializeField]
        bool countSplittedAmmo = false;

        int currentAmmo;
        float cd => 1f / bulletPerSec;
        bool canAttack = true;
        Coroutine reloadCR;
        public override void Initialize(params object[] args)
        {
            base.Initialize(args);
            currentAmmo = clipSize;
            canAttack = true;
        }

        public override void Fire(Transform pos, Vector2 direction)
        {
            if (currentAmmo <= 0)
            {
                Reload();
            }
            else
            {
                if (canAttack)
                {
                    //float totalAngle = (splitAmount - 1) * splitAngle;
                    //Vector2 startingDirection = Quaternion.AngleAxis(-totalAngle / 2f, Vector3.right) * direction;

                    //float theta = Mathf.Acos(Vector2.Dot(direction, Vector2.right) / (direction.magnitude));
                    //float directionAngle = Mathf.Rad2Deg * theta;
                    //float startAngle = directionAngle + totalAngle / 2f;

                    //for (int i = 0; i < splitAmount; i++)
                    //{
                    //    Vector2 pdir = Quaternion.AngleAxis(splitAngle * i, Vector3.forward) * startingDirection;
                    Projectile p = Instantiate(projectileTemplate, pos.position, Quaternion.identity);

                    p.transform.right = direction;
                    p.Launch(direction);
                    //}

                    currentAmmo--;
                    canAttack = false;
                    GameCore.GameManager.Instance.StartCoroutine(CDTimerCR());
                }
            }
        }

        public void Reload()
        {
            if (reloadCR == null)
            {
                GameCore.GameManager.Instance.StartCoroutine(ReloadCR());
            }
            IEnumerator ReloadCR()
            {
                yield return new WaitForSeconds(reloadTime);
                currentAmmo = clipSize;
                reloadCR = null;
            }
        }

        IEnumerator CDTimerCR()
        {
            yield return new WaitForSeconds(cd);
            canAttack = true;
        }
    }
}
