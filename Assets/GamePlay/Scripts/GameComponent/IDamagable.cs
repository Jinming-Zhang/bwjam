using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public enum DamageType
    {
        Health,
        Clue,
        AllYouCanThinkOf
    }
    void TakeDamage(float amount, MonoBehaviour source, DamageType damageType = DamageType.Health);
}
