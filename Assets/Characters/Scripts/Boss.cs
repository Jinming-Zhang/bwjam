using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;

public class Boss : MonoBehaviour
{
    [SerializeField]
    MovementBehaviour defaultMovementBehaviourTemplate;
    [SerializeField]
    Transform defaultWeaponTransform;
    AIBossAbilityHitMovementBehavour abilityHitBehaviour;
    MovementBehaviour currentMovementBehaviour;
    //[SerializeField]
    //AttackBehaviour attackBehaviour;

    PlayerController player;
    PlayerController Player
    {
        get
        {
            if (!player)
            {
                GameObject go = GameObject.FindWithTag("Player");
                if (go)
                {
                    player = go.GetComponent<PlayerController>();
                }
            }
            return player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMovementBehaviour = Instantiate(defaultMovementBehaviourTemplate);
        currentMovementBehaviour.Initialize(gameObject, defaultWeaponTransform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (currentMovementBehaviour)
        {
            currentMovementBehaviour.UpdateMovement();
        }
    }
    public void DefaultProjectileHitPlayer()
    {
        currentMovementBehaviour = null;

    }
}
