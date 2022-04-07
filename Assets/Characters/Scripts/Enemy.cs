using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    AIMovementBehaviour movementBehaviour;

    private void Start()
    {
        //movementBehaviour.Initialize(gameObject);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        movementBehaviour.UpdateMovement();
    }
}
