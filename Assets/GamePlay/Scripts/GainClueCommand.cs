using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainClueCommand : ICommand
{
    int delta;
    public GainClueCommand(int delta)
    {
        this.delta = delta;
    }

    public void Execute()
    {
        GamePlay.PlayerController player = GameCore.GameManager.Instance.Player;
        if (player)
        {
            player.TakeDamage(delta, null, IDamagable.DamageType.Clue);
        }
    }
}
