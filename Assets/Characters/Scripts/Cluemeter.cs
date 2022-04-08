using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluemeter : MonoBehaviour
{
    [SerializeField]
    int initial = 0;
    int current;
    public int Value { get => current; set => current = value; }

    // Start is called before the first frame update
    void Start()
    {
        current = initial;
    }
}
