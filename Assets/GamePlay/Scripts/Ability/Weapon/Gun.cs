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
        AudioClip reloadClip;
        [SerializeField]
        protected int clipSize;
        public int ClipSize => clipSize;
        [SerializeField]
        protected float reloadTime;
        [SerializeField]
        protected Projectile projectileTemplate;

        [Header("Ammo Splitting")]
        [SerializeField]
        int initialSplit = 1;
        [HideInInspector]
        public int splitAmount;
        [SerializeField]
        float splitAngle = 10f;

        int currentAmmo;
        public int CurrentAmmo => currentAmmo;
        public float cd => 1f / bulletPerSec;
        bool canAttack = true;
        protected Coroutine reloadCR;
        public override void Initialize(GameObject owner, params object[] args)
        {
            base.Initialize(owner, args);
            currentAmmo = clipSize;
            canAttack = true;
            splitAmount = initialSplit;
            if (!needReload)
            {
                currentAmmo = int.MaxValue;
            }
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
                    Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, 0, -totalAngle / 2f));
                    Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 0, splitAngle));
                    Vector3 bulletDirection = rotateLeft * direction.normalized;
                    base.Fire(pos, direction);
                    ShotBullet(bulletDirection);
                    for (int i = 1; i < splitAmount; i++)
                    {
                        bulletDirection = rotateRight * bulletDirection.normalized;
                        ShotBullet(bulletDirection);
                    }

                    currentAmmo--;
                    canAttack = false;
                    GameCore.GameManager.Instance.StartCoroutine(CDTimerCR());
                }
                void ShotBullet(Vector3 dir)
                {
                    Projectile p = Instantiate(projectileTemplate, pos.position, Quaternion.Euler(dir));

                    p.Initiailze(owner);
                    p.transform.right = dir;
                    p.Launch(dir);
                }
            }
        }

        public virtual void Reload()
        {
            if (reloadCR == null)
            {
                if (reloadClip)
                {
                    WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(reloadClip);
                }
                reloadCR = GameCore.GameManager.Instance.StartCoroutine(ReloadCR());
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
