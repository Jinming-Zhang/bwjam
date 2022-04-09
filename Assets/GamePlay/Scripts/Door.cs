using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractibleObject
{
    [SerializeField]
    Collider2D doorCollider;

    [SerializeField]
    Animator animator;

    bool closed = true;
    public override void Interact(GameObject initiator)
    {
        closed = !closed;
        doorCollider.enabled = closed;
        if (closed)
        {
            animator.Play(AnimationConstants.Door_Close);
        }
        else
        {
            animator.Play(AnimationConstants.Door_Open);
        }
    }
}
