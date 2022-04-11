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
        Enemy me;
        public override void Initialize(GameObject src, params object[] args)
        {
            base.Initialize(src, args);
            canAttack = true;
            me = owner.GetComponent<Enemy>();
        }

        public override void Fire(Transform pos, Vector2 direction)
        {
            if (canAttack)
            {
                base.Fire(pos, direction);
                me.DoAttackAnimation();
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
