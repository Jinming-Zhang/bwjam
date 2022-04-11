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
    public float attackRange;

    [SerializeField]
    UnityEngine.UI.Image healthBar;

    [SerializeField]
    GameObject graphics;
    public GameObject Graphics => graphics;
    [SerializeField]
    Animator animator;
    public Animator Animator => animator;
    AIMovementBehaviour movementBehaviour;
    protected bool dead = false;


    private void Start()
    {
        movementBehaviour = ScriptableObject.Instantiate(movementBehaviourTemplate);
        movementBehaviour.Initialize(gameObject, weaponTransform);
        healthBar.fillAmount = 1;
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            movementBehaviour.UpdateMovement();
        }
    }

    public virtual void Die()
    {
        if (!dead)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            dead = true;
            GameCore.GameManager.Instance.OnPlayerKilledEnemy(this);
            GameStatus.OnPlayerKilledEnemy();
            OnDead();
        }
    }
    public virtual void OnDead()
    {

    }
    public virtual void OnDeadAnimationFinished()
    {
        Destroy(gameObject);
    }

    public virtual void DoIdleAnimation()
    {

    }
    public virtual void DoAttackAnimation()
    {

    }
    public virtual void DoDamagedAnimation()
    {

    }
    public virtual void DoWalkAnimation()
    {

    }
    public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
    {
        health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
        healthBar.fillAmount = health.Value / (float)health.MaxValue;
        DoDamagedAnimation();
        if (health.Value <= 0)
        {
            Die();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponTransform.position, attackRange);
    }
}

