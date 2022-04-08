using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySpeedMultipierCommand : ICommand
{
    PlayerController player;
    float originalMultiplier;
    float multiplier;

    public ApplySpeedMultipierCommand(PlayerController player, float multiplier)
    {
        this.player = player;
        this.multiplier = multiplier;
    }
    public void Execute()
    {
        this.originalMultiplier = player.currentSpeedMultiplier;
        player.ApplySpeedMultiplier(multiplier);
        player.attackable = false;
    }
    public void Undo()
    {
        player.ApplySpeedMultiplier(originalMultiplier);
        player.attackable = true;
    }
}
