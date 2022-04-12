using Cinemachine;
using GamePlay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfUISystem;
using WolfUISystem.Presets;
using static UnityEngine.InputSystem.InputAction;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour, IPausableComponent, IDamagable
    {
        public enum FaceDirection
        {
            Left,
            Right,
            None
        }

        private static PlayerController instance;
        public static PlayerController Instance => instance;
        [SerializeField]
        GameObject graphics;
        [SerializeField]
        Animator animator;
        public Animator Animator { get => animator; }
        [SerializeField]
        public PlayerMovementBehaviour moveBehaviour;
        [SerializeField]
        PlayerAttackBehaviour attackBehaviour;
        [SerializeField]
        Transform weaponPosition;
        [SerializeField]
        Health health;
        public Health Health => health;
        [SerializeField]
        public Cluemeter cluemeter;
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        AudioClip feetstepl;
        [SerializeField]
        AudioClip feetstepr;
        HudScreen hud;
        public Weapon CurrentWeapon => attackBehaviour.CurrentWeapon;

        bool paused;
        bool controllable;
        bool dead = false;
        public float currentSpeedMultiplier => moveBehaviour.speedMultiplier;
        public bool attackable { get => attackBehaviour.Attackable; set => attackBehaviour.Attackable = value; }
        [SerializeField]
        float dmgCamShakeDuration = .3f;
        float camShakeCDCounter;
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
            InitializePlayer();
            InitializeUI();
            void InitializePlayer()
            {
                controllable = true;
                dead = false;
                camShakeCDCounter = 0;
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
            void InitializeUI()
            {
                hud = UIManager.Instance.GetScreenComponent<HudScreen>();
                hud.UpdatePlayerHealth(health.Value);
                hud.UpdateClumeter(cluemeter.Value, cluemeter.MaxValue);
                hud.UpdateAmmo(attackBehaviour.myGun.CurrentAmmo, attackBehaviour.myGun.ClipSize);
                hud.ReloadDone();
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (!paused && controllable)
            {
                attackBehaviour.Update();
            }
            if (attackBehaviour.faceDirectionPreference != FaceDirection.None)
            {
                FaceToward(attackBehaviour.faceDirectionPreference);
            }
            else
            {
                FaceToward(moveBehaviour.faceDirectionPreference);
            }
        }

        private void FixedUpdate()
        {
            if (!paused && controllable)
            {
                moveBehaviour.UpdateMovement();
            }
        }

        void FaceToward(FaceDirection direction)
        {
            graphics.transform.rotation = Quaternion.Euler(0, direction == FaceDirection.Left ? 180 : 0, 0);
        }
        public void Pause()
        {
            paused = true;
        }

        public void Resume()
        {
            paused = false;
        }

        public void TakeDamage(float amount, MonoBehaviour source, IDamagable.DamageType damageType = IDamagable.DamageType.Health)
        {
            if (amount > 0 && camShakeCDCounter <= 0 && !dead)
            {
                StartCoroutine(CamShakeCR());
            }
            if (damageType == IDamagable.DamageType.Health)
            {
                if (cluemeter.Value > 0)
                {
                    cluemeter.Value = cluemeter.Value - Mathf.FloorToInt(amount);
                    WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.gaugeDecrease);
                    PlayHitSFX();
                }
                else
                {
                    health.Value = Mathf.Max(0, health.Value - Mathf.FloorToInt(amount));
                    OnHealthChanged(health.Value);
                }
            }
            else if (damageType == IDamagable.DamageType.Clue)
            {
                cluemeter.Value = cluemeter.Value - Mathf.FloorToInt(amount);
                WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.gaugeDecrease);
                PlayHitSFX();
            }

            void OnHealthChanged(int newHealth)
            {
                if (newHealth == 0)
                {
                    if (!dead)
                    {
                        dead = true;
                        WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.PlayerDead);
                        GameStatus.OnPlayerDead();
                        GameCore.GameManager.Instance.OnPlayerDead();
                    }
                }
                else
                {
                    if (!dead)
                    {
                        PlayHitSFX();
                    }
                }
                hud.UpdatePlayerHealth(newHealth);
            }
        }
        void PlayHitSFX()
        {
            if (!dead)
            {
                WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.PlayerHit);
            }
        }
        public void Charmed(GameObject src, float duration, float speed)
        {
            controllable = false;
            StopAllCR();

            Vector2 direction = src.transform.position - gameObject.transform.position;
            rb.velocity = direction.normalized * speed;
            StartCoroutine(CharedCR());

            IEnumerator CharedCR()
            {
                float timer = duration;
                while (timer > 0)
                {
                    timer -= Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
                controllable = true;
                rb.velocity = Vector2.zero;
            }
        }
        public void ForcePush(Vector2 distination, float speed)
        {
            controllable = false;
            StopAllCR();
            StartCoroutine(ForcePushedCR());

            IEnumerator ForcePushedCR()
            {
                yield return new WaitForFixedUpdate();
                Vector2 directionalDistance = distination - new Vector2(transform.position.x, transform.position.y);
                float time = directionalDistance.magnitude / speed;
                rb.velocity = directionalDistance.normalized * speed;

                yield return new WaitForSeconds(time);
                rb.velocity = Vector2.zero;
                controllable = true;
            }
        }

        void StopAllCR()
        {
            GameObject scenevcgo = GameObject.FindGameObjectWithTag("SceneVC");
            if (scenevcgo)
            {
                CinemachineVirtualCamera vc = scenevcgo.GetComponent<CinemachineVirtualCamera>();
                CinemachineBasicMultiChannelPerlin noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
                camShakeCDCounter = 0;
            }
            StopAllCoroutines();
        }

        IEnumerator CamShakeCR()
        {
            GameObject scenevcgo = GameObject.FindGameObjectWithTag("SceneVC");
            if (scenevcgo)
            {
                camShakeCDCounter = dmgCamShakeDuration;
                CinemachineVirtualCamera vc = scenevcgo.GetComponent<CinemachineVirtualCamera>();
                CinemachineBasicMultiChannelPerlin noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noise.m_AmplitudeGain = 0.2f;
                noise.m_FrequencyGain = 60;
                yield return new WaitForSeconds(dmgCamShakeDuration);
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
                camShakeCDCounter = 0;
            }
        }
        public void ApplySpeedMultiplier(float multiplier)
        {
            moveBehaviour.speedMultiplier = multiplier;
        }
        public void WalkLeft()
        {
            WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(feetstepl);
        }
        public void WalkRight()
        {
            WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(feetstepr);
        }
    }
}