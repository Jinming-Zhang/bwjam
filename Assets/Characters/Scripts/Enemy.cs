using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Health health;
    [SerializeField]
    AIMovementBehaviour movementBehaviourTemplate;
    [SerializeField]
    Transform weaponTransform;

    AIMovementBehaviour movementBehaviour;
    private void Start()
    {
        movementBehaviour = ScriptableObject.Instantiate(movementBehaviourTemplate);
        movementBehaviour.Initialize(gameObject, weaponTransform);
        health.OnHealthReached0 = () => Die();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        movementBehaviour.UpdateMovement();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
