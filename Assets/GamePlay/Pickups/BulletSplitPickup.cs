using GamePlay;
using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletSplitPickup : MonoBehaviour
{
    [SerializeField]
    GameObject pickupUI;
    [SerializeField]
    int splitDelta = 0;
    [SerializeField]
    bool respawnable = true;
    [SerializeField]
    float respawnInterval = 5f;

    private void Start()
    {
        pickupUI.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            if (player.CurrentWeapon is Gun gun)
            {
                new BulletSplitUpdateCommand(gun, splitDelta).Execute();
                OnPicupConsumed();
            }
        }
    }
    void OnPicupConsumed()
    {
        pickupUI.gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        if (respawnable)
        {
            StartCoroutine(RespawnCR());
        }
        IEnumerator RespawnCR()
        {
            yield return new WaitForSeconds(respawnInterval);
            GetComponent<Collider2D>().enabled = true;
            pickupUI.gameObject.SetActive(true);
        }
    }
}
