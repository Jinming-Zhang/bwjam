using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WolfUISystem;
using WolfUISystem.Presets;
using UnityEngine.SceneManagement;
using GamePlay;
using WolfAudioSystem;
using GamePlay.Weapons;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance => instance;

        [SerializeField]
        PlayerInput playerUIInput;
        public GameResourceLocator ResourceLocator;
        public LevelManager currentLevelManager;
        PlayerController player;
        public PlayerController Player
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
        public GameProgressTracker progressTracker;

        [SerializeField]
        bool ingame = false;
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
            if (SceneManager.GetActiveScene().name.Equals("IntroScene"))
            {
                GameSequence.TransitionToIntroScene();
                ingame = false;
            }
            else
            {
                ingame = true;
            }
        }

        public void StartGame()
        {
            progressTracker.ResetStatus();
            GameSequence.StartLevel1();
            ingame = true;
        }
        public void BackToIntroScreen()
        {
            progressTracker.ResetStatus();
            GameSequence.BackToIntroScreen();
            ingame = false;
        }
        public void ProgressToNextScene()
        {
            string sceneName = Instance.progressTracker.GetNextRoom(player.cluemeter.Value, out bool fightBoss, out bool startFromBeginning);
            if (fightBoss)
            {
                AudioSystem.Instance.TransitionBGMQuick(ResourceLocator.audioSetup.BossFight);
            }
            else if (startFromBeginning)
            {
                ApplyPerks(player.cluemeter.Value);
                player.cluemeter.Value = 0;
            }
            GameSequence.SwitchGameplayScene(sceneName);
        }
        void ApplyPerks(int clueamount)
        {
            if (player.CurrentWeapon is Gun gun)
            {
                new BulletSplitUpdateCommand(gun, 1).Execute();
            }
        }
        public void OnPlayerDead()
        {
            player.Health.Value = player.Health.MaxValue;
        }

        public void OnPlayerKilledEnemy(Enemy killed)
        {
            player.cluemeter.Value++;
            currentLevelManager.OnEnemyDead();
        }

        int pauseRequestCount = 0;
        public void PauseGame()
        {
            if (!ingame)
            {
                return;
            }
            pauseRequestCount++;
            Time.timeScale = 0;
            //if (currentLevelManager != null)
            //{
            //    currentLevelManager.PauseLevel();
            //}
            //else
            //{
            //    Debug.LogWarning("GameManager: Failed pause game, no level manager found");
            //}
        }
        public void ResumeGame()
        {
            if (!ingame)
            {
                return;
            }
            pauseRequestCount--;
            if (pauseRequestCount > 0)
            {
                Debug.LogWarning($"GameManager: Consumed 1 Resume Request, {pauseRequestCount} left");
                return;
            }
            Time.timeScale = 1;
            //if (currentLevelManager != null)
            //{
            //currentLevelManager.ResumeLevel();
            //}
            //else
            //{
            //    Debug.LogWarning("GameManager: Failed pause game, no level manager found");
            //}
        }

        public void OnToggleIngameUIPressed(InputAction.CallbackContext ctx)
        {
            if (!ingame)
            {
                return;
            }
            if (ctx.performed)
            {
                ScreenBase currentTopScreen = UIManager.Instance.GetCurrentScreen();
                if (currentTopScreen.GetType() != typeof(HudScreen))
                {
                    UIManager.Instance.PopScreen();
                }
                else
                {
                    PopupScreen p = UIManager.Instance.PushScreen<PopupScreen>();
                }
            }
        }
    }
}