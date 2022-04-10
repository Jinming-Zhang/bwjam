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
    [SerializeField]
    UnityEngine.UI.Image healthBar;

    [SerializeField]
    GameObject graphics;
    public GameObject Graphics => graphics;
    [SerializeField]
    Animator animator;
    public Animator Animator => animator;
    AIMovementBehaviour movementBehaviour;
    bool dead = false;
    private void Start()
    {
        movementBehaviour = ScriptableObject.Instantiate(movementBehaviourTemplate);
        movementBehaviour.Initialize(gameObject, weaponTransform);
        healthBar.fillAmount = 1;
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
        if (!dead)
        {
            dead = true;
            GameCore.GameManager.Instance.Player.cluemeter.Value++;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
    {
        health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
        healthBar.fillAmount = health.Value / (float)health.MaxValue;
        if (health.Value <= 0)
        {
            Die();
            GameStatus.OnPlayerKilledEnemy();
        }
    }
}
