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
        protected float initialSpeed;
        protected float ingameMovingSpeed;

        [Header("Attack")]
        [SerializeField]
        protected float attackRange;
        [SerializeField]
        protected Weapon weaponTemplate;
        protected Weapon weapon;

        [Header("Config")]
        [SerializeField]
        protected bool updateIngameSpeed = true;
        protected PlayerController player;
        protected PlayerController Player
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

        protected Rigidbody2D _rb;
        protected Rigidbody2D rb
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

        protected Transform weaponTransform;
        public override void Initialize(GameObject owner, params object[] args)
        {
            base.Initialize(owner, args);
            ingameMovingSpeed = initialSpeed;
            if (args.Length > 0 && args[0] is Transform wpnTran)
            {
                weaponTransform = wpnTran;
            }
            weapon = Instantiate(weaponTemplate);
            weapon.Initialize(owner);
        }

        public override void UpdateMovement()
        {
            if (updateIngameSpeed)
            {
                ingameMovingSpeed = initialSpeed;
            }

            if (Player)
            {
                Vector2 tarDirection = Player.transform.position - weaponTransform.position;
                // out of range, move to player
                if (tarDirection.magnitude > attackRange)
                {
                    rb.velocity = tarDirection.normalized * ingameMovingSpeed;
                }
                // attack player
                else
                {
                    rb.velocity = Vector2.zero;
                    weapon.Fire(weaponTransform, tarDirection);
                }
            }
        }
    }
}
