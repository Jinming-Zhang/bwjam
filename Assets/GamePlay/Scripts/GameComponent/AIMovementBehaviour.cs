using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.Weapons;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Movement/AI Movement", fileName = "AI Movement Behaviour")]
    public class AIMovementBehaviour : MovementBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        float initialSpeed;
        float ingameMovingSpeed;

        [Header("Attack")]
        [SerializeField]
        float attackRange;
        [SerializeField]
        Weapon weapon;

        [Header("Config")]
        bool updateIngameSpeed = true;
        PlayerController player;
        PlayerController Player
        {
            get
            {
                if (!player)
                {

                    GameObject go = GameObject.FindGameObjectWithTag("Player");
                    if (go)
                    {
                        player = go.GetComponent<PlayerController>();
                    }
                }
                return player;
            }
        }

        Rigidbody2D _rb;
        Rigidbody2D rb
        {
            get
            {
                if (!_rb)
                {
                    _rb = owner.GetComponent<Rigidbody2D>();
                }
                return _rb;
            }
        }

        public override void Initialize(GameObject owner, object args)
        {
            base.Initialize(owner, args);
            ingameMovingSpeed = initialSpeed;
            weapon.Initialize();
        }

        public override void UpdateMovement()
        {
            if (updateIngameSpeed)
            {
                ingameMovingSpeed = initialSpeed;
            }

            if (Player)
            {
                Vector2 tarDirection = Player.transform.position - owner.transform.position;
                // out of range, move to player
                if (tarDirection.magnitude > attackRange)
                {
                    Vector2 velocity = Player.gameObject.transform.position - owner.gameObject.transform.position;
                    rb.velocity = velocity * ingameMovingSpeed;
                }
                // attack player
                else
                {
                    rb.velocity = Vector2.zero;
                    weapon.Fire(owner.transform, tarDirection);
                }
            }
        }
    }
}
