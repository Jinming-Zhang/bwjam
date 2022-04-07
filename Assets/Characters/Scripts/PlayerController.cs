using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour, IPausableComponent, IDamagable
    {
        private static PlayerController instance;
        public static PlayerController Instance => instance;
        [SerializeField]
        PlayerMovementBehaviour moveBehaviour;
        [SerializeField]
        PlayerAttackBehaviour attackBehaviour;
        [SerializeField]
        Transform weaponPosition;
        [SerializeField]
        Health health;
        [SerializeField]
        Cluemeter cluemeter;

        public Weapon CurrentWeapon => attackBehaviour.CurrentWeapon;

        bool paused;
        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            Initialize();
            void Initialize()
            {
                PlayerInput playerInput = GetComponent<PlayerInput>();
                if (playerInput)
                {
                    playerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
                    playerInput.SwitchCurrentActionMap("Player");
                    moveBehaviour.Initialize(gameObject, playerInput);
                    attackBehaviour.Initialize(gameObject, weaponPosition, playerInput);
                }
                else
                {
                    Debug.LogError("PlayerController: Could not found PlayerInput to initialize PlayerMovementBehaviour");
                }
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (!paused)
            {
                attackBehaviour.Update();
            }
        }

        private void FixedUpdate()
        {
            if (!paused)
            {
                moveBehaviour.UpdateMovement();
            }
        }
        public void Pause()
        {
            paused = true;
        }

        public void Resume()
        {
            paused = false;
        }

        public void TakeDamage(float amount, MonoBehaviour source)
        {
            if (source is BossDefaultWeaponProjectile bossDefaultProjectile)
            {
                Debug.Log("Player seduced by boss o 0");
            }
            else
            {
                Debug.Log("Player got hit");
            }
        }

    }
}