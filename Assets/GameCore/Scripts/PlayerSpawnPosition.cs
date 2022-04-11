using GameCore;
using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController player = GameManager.Instance.Player;
        if (player)
        {
            player.GetComponent<Rigidbody2D>().Sleep();
            player.transform.position = gameObject.transform.position;
            player.GetComponent<Rigidbody2D>().WakeUp();
        }
    }
}
