using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfAudioSystem;

public static class GameStatus
{
    private static GameAudioSetup audioSetup => GameCore.GameManager.Instance.ResourceLocator.audioSetup;
    private static int consecutiveEnemyKilled = 0;
    public static void OnPlayerKilledEnemy()
    {
        consecutiveEnemyKilled++;
        if (consecutiveEnemyKilled == 1)
        {
            AudioSystem.Instance.TransitionBGMQuick(audioSetup.EnemyKill1, true);
        }
        else if (consecutiveEnemyKilled == 2)
        {
            AudioSystem.Instance.TransitionBGMQuick(audioSetup.EnemyKill2, true);
        }
        else if (consecutiveEnemyKilled == 3)
        {
            AudioSystem.Instance.TransitionBGMQuick(audioSetup.EnemyKill3, true);
        }
        else if (consecutiveEnemyKilled == 4)
        {
            AudioSystem.Instance.TransitionBGMQuick(audioSetup.EnemyKill4, true);
        }
    }

    public static void OnPlayerDead()
    {
        consecutiveEnemyKilled = 0;
    }
}
