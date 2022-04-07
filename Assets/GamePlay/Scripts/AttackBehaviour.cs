using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : ScriptableObject
{
    protected GameObject owner;
    [SerializeField]
    protected Weapon weapon;
    protected Transform weaponPos;
    public Weapon CurrentWeapon => weapon;
    public virtual void Initialize(GameObject owner, Transform weaponPos, params object[] args)
    {
        this.owner = owner;
        this.weaponPos = weaponPos;
        weapon.Initialize(owner);
    }

    public virtual void Update()
    {
    }

    public virtual void Attack()
    {
        weapon.Fire(weaponPos, weaponPos.forward);
    }
}
