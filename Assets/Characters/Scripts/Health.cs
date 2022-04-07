using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField]
    int initial = 3;
    int current;
    public int Value => current;

    public System.Action OnHealthReached0 = null;
    // Start is called before the first frame update
    void Start()
    {
        current = initial;
    }

    public void TakeDamage(float amount, MonoBehaviour source)
    {
        current = Mathf.Max(0, current - Mathf.FloorToInt(amount));
        OnHealthReached0?.Invoke();
    }
}
