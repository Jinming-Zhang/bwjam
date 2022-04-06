using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour, IPausableComponent
    {
        private static PlayerController instance;
        public static PlayerController Instance => instance;
        [SerializeField]
        PlayerMovementBehaviour moveBehaviour;
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
            PlayerInput playerInput = GetComponent<PlayerInput>();
            if (playerInput)
            {
                moveBehaviour.Initialize(gameObject, playerInput);
            }
            else
            {
                Debug.LogError("PlayerController: Could not found PlayerInput to initialize PlayerMovementBehaviour");
            }
        }

        // Update is called once per frame
        void Update()
        {
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
    }
}