using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelGate : InteractibleObject
{
    [SerializeField]
    string LevelName;

    public override void Interact(GameObject initiator)
    {
        GotoNextLevel();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!needInteraction)
        {
            if (collision.GetComponent<GamePlay.PlayerController>())
            {
                GotoNextLevel();
            }
        }
    }

    private void GotoNextLevel()
    {
        if (string.IsNullOrEmpty(LevelName))
        {
            GameCore.GameManager.Instance.ProgressToNextScene();
        }
        else
        {
            GameSequence.SwitchGameplayScene(LevelName);
        }
    }
}
