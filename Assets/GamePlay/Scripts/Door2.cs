using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{


    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject enableObject;


    bool closed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            Close(false);
            enableObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            Close(true);
            enableObject.SetActive(false);
        }
    }

    private void Close(bool isClose)
    {
        if (isClose)
        {
            animator.Play(AnimationConstants.Door_Close);
        }
        else
        {
            animator.Play(AnimationConstants.Door_Open);
        }
    }
}
