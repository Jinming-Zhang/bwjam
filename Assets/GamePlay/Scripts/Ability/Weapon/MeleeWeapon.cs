using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Weapons
{
    [CreateAssetMenu(menuName = "Weapon/Melee Weapon", fileName = "Melee Weapon")]
    public class MeleeWeapon : Weapon
    {
        [SerializeField]
        protected float attackPerSec;
        [SerializeField]
        protected GameObject weaponTemplate;

        float cd => 1f / attackPerSec;
        bool canAttack = true;

        public override void Initialize(params object[] args)
        {
            base.Initialize(args);
            canAttack = true;
        }

        public override void Fire(Transform pos, Vector2 direction)
        {
            if (canAttack)
            {
                base.Fire(pos, direction);
                Instantiate(weaponTemplate, pos);
                canAttack = false;
                GameCore.GameManager.Instance.StartCoroutine(CDTimerCR());
            }
        }
        IEnumerator CDTimerCR()
        {
            yield return new WaitForSeconds(cd);
            canAttack = true;
        }
    }
}
