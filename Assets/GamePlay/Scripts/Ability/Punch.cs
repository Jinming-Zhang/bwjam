using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField]
    float lifetime = 0.05f;
    [SerializeField]
    float dmg;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();
        if (target != null)
        {
            target.TakeDamage(dmg, this);
        }
    }
}
