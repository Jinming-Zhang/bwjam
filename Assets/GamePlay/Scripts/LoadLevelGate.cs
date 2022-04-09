using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelGate : MonoBehaviour
{
    [SerializeField]
    string levelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GamePlay.PlayerController>())
        {
            GameSequence.SwitchGameplayScene(levelName);
        }
    }
}
