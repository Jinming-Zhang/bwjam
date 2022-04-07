using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;
using GamePlay.Weapons;
using System;

public class AIBossAbilityHitMovementBehavour : MovementBehaviour
{
    [SerializeField]
    Weapon bossEnoughNearWeapon;
    [SerializeField]
    Weapon bossNotNearEnougWeapon;

    [SerializeField]
    System.Action onWeaponLaunched;
    public override void Initialize(GameObject owner, params object[] args)
    {
        base.Initialize(owner, args);
        if (args.Length != 3)
        {
            logError();
            return;
        }
        else
        {
            try
            {
                bossEnoughNearWeapon = Instantiate(args[0] as Weapon);
                bossNotNearEnougWeapon = Instantiate(args[1] as Weapon);
                onWeaponLaunched = args[3] as System.Action;
            }
            catch (Exception)
            {
                logError();
                return;
            }
        }

        void logError()
        {
            Debug.LogError($"{GetType()}: Wrong initialize parameter. p1: Near Enough Weapon, p2: Not Near Engouh Weapon, p3: Weapon Launched Callback.");
        }
    }

    public override void UpdateMovement()
    {
    }
}
