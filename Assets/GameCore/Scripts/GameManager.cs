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
        public List<GameObject> enemyPoop;
        [SerializeField]
        bool ingame = false;
        int unluckyCounter = 0;
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
            AudioSystem.Instance.TransitionBGMQuick(ResourceLocator.audioSetup.IntroClip);
        }
        public void Initialize()
        {
            unluckyCounter = 0;
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
            unluckyCounter = 0;
            progressTracker.ResetStatus();
            GameSequence.BackToIntroScreen();
            ingame = false;
        }
        public void ProgressToNextScene()
        {
            string sceneName = Instance.progressTracker.GetNextRoom(player.cluemeter.Value, out bool fightBoss, out bool startFromBeginning);
            if (fightBoss)
            {
                if (unluckyCounter == 0)
                {
                    ApplyPerks(player.cluemeter.Value);
                }
                AudioSystem.Instance.TransitionBGMQuick(ResourceLocator.audioSetup.BossFight);
            }
            else if (startFromBeginning)
            {
                unluckyCounter++;
                ApplyPerks(player.cluemeter.Value);
                Player.cluemeter.Value = 0;
            }
            GameSequence.SwitchGameplayScene(sceneName);
        }
        void ApplyPerks(int clueamount)
        {
            int cluecounter = clueamount;
            if (Player.CurrentWeapon is Gun gun)
            {
                new BulletSplitUpdateCommand(gun, 1).Execute();
            }
            cluecounter -= 2;
            while (cluecounter > 0)
            {
                Player.moveBehaviour.ingameMoveSpeed += .2f;
                cluecounter -= 2;
                if (cluecounter < 0)
                {
                    break;
                }
                (Player.CurrentWeapon as Gun).bulletPerSecInGame += 1f;
                cluecounter -= 2;
            }
        }
        public void OnPlayerDead()
        {
            GameSequence.GameEnd(false);
        }

        public void OnPlayerKilledEnemy(Enemy killed)
        {
            player.cluemeter.Value++;
            WolfAudioSystem.AudioSystem.Instance.PlaySFXOnCamera(GameCore.GameManager.Instance.ResourceLocator.audioSetup.gagueIncrease);
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

        public void GameFinish(bool success)
        {
            GameSequence.GameEnd(success);
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