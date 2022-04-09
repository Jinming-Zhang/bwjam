using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluemeter : MonoBehaviour
{
    [SerializeField]
    int initial = 0;
    [SerializeField]
    int max = 10;
    int current;
    public int Value { get => current; set => current = value; }
    public int MaxValue { get => max; }

    // Start is called before the first frame update
    void Start()
    {
        current = initial;
    }
}
