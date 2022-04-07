using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplitUpdateCommand : ICommand
{
    Gun gun;
    int delta;
    public BulletSplitUpdateCommand(Gun gun, int increment = 1)
    {
        this.gun = gun;
        this.delta = increment;
    }
    public void Execute()
    {
        gun.splitAmount = Mathf.Max(1, gun.splitAmount + delta);
    }
}
