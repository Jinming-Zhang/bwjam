using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

public class Enemy : MonoBehaviour, IDamagable
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

    public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
    {
        health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
        if (health.Value <= 0)
        {
            Die();
        }
    }
}
