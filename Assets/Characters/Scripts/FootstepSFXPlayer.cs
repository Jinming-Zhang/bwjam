using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSFXPlayer : MonoBehaviour
{
    [SerializeField]
    GamePlay.PlayerController player;
    public void Left()
    {
        player.WalkLeft();
    }
    public void Right()
    {
        player.WalkRight();
    }
}
