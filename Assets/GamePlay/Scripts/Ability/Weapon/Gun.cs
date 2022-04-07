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
        bool needReload = true;
        [SerializeField]
        protected int clipSize;
        [SerializeField]
        protected float reloadTime;
        [SerializeField]
        protected Projectile projectileTemplate;

        [Header("Ammo Splitting")]
        [SerializeField]
        public int splitAmount = 1;
        [SerializeField]
        float splitAngle = 10f;

        int currentAmmo;
        float cd => 1f / bulletPerSec;
        bool canAttack = true;
        Coroutine reloadCR;
        public override void Initialize(GameObject owner, params object[] args)
        {
            base.Initialize(owner, args);
            currentAmmo = clipSize;
            canAttack = true;
        }

        public override void Fire(Transform pos, Vector2 direction)
        {
            if (currentAmmo <= 0)
            {
                if (needReload)
                {
                    Reload();
                }
                else
                {
                    currentAmmo = clipSize;
                }
            }
            else
            {
                if (canAttack)
                {
                    float totalAngle = (splitAmount - 1) * splitAngle;
                    Vector2 startDir = Quaternion.AngleAxis(-totalAngle / 2f, Vector3.forward) * direction.normalized;

                    for (int i = 0; i < splitAmount; i++)
                    {
                        startDir = Quaternion.AngleAxis(splitAngle * i, Vector3.forward) * startDir;
                        Projectile p = Instantiate(projectileTemplate, pos.position, Quaternion.Euler(startDir));

                        p.Initiailze(owner);
                        p.transform.right = startDir;
                        p.Launch(startDir);
                    }

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
