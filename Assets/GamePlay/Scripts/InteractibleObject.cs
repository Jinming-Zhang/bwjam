using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour, IIteractable
{
    [SerializeField]
    float interactionRange = 1f;
    [SerializeField]
    GameObject icon;
    public abstract void Interact(GameObject initiator);

    private void Start()
    {
        icon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            player.InteractTarget = this;
            icon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            player.InteractTarget = null;
            icon.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
