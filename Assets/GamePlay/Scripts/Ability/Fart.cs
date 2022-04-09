using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Fart : MonoBehaviour
{
    static bool AppliedToPlayer = false;
    [SerializeField]
    float slowMultiplier;
    [SerializeField]
    float lifetime = 5f;
    ApplySpeedMultipierCommand cmd;
    [SerializeField]
    float moveSpeed = 3f;
    Vector3 targetPosition;
    private IEnumerator Start()
    {
        targetPosition = GameCore.GameManager.Instance.Player.transform.position;

        cmd = null;
        yield return new WaitForSeconds(lifetime);
        if (cmd != null)
        {
            cmd.Undo();
            AppliedToPlayer = false;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AppliedToPlayer)
        {
            return;
        }
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            cmd = new ApplySpeedMultipierCommand(p, slowMultiplier);
            cmd.Execute();
            AppliedToPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            if (cmd != null)
            {
                cmd.Undo();
                AppliedToPlayer = false;
                cmd = null;
            }
        }
    }
}
