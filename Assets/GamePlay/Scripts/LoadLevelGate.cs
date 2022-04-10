using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelGate : MonoBehaviour
{
    [SerializeField]
    string LevelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GamePlay.PlayerController>())
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
}
