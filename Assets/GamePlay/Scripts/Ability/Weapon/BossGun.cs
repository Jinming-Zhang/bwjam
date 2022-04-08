using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.Weapons;
[CreateAssetMenu(menuName = "Weapon/Boss Gun", fileName = "Boss Gun")]
public class BossGun : Gun
{
    public override void Fire(Transform pos, Vector2 direction)
    {
        base.Fire(pos, direction);
        owner.GetComponent<Boss>().SwitchToDefaultMoveBehaviour();
    }
}
