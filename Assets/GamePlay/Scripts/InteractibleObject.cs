using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObject : MonoBehaviour, IIteractable
{
    [SerializeField]
    protected bool needInteraction = true;
    [SerializeField]
    GameObject icon;
    public abstract void Interact(GameObject initiator);

    private void Start()
    {
        icon.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player & needInteraction)
        {
            player.InteractTarget = this;
            icon.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player & needInteraction)
        {
            player.InteractTarget = null;
            icon.SetActive(false);
        }
    }
}
