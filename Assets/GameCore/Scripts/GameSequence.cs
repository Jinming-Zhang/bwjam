using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using WolfAudioSystem;
using WolfUISystem.Presets;
using GameCore;
using UnityEngine.SceneManagement;
using GamePlay;

public static class GameSequence
{
    static GameAudioSetup audioSetup => GameManager.Instance.ResourceLocator.audioSetup;
    public static void TransitionToIntroScene()
    {
        IntroScreen intro = UIManager.Instance.PopAllAndSwitchToScreen<IntroScreen>();
        TransitionScreen t = UIManager.Instance.PushScreen<TransitionScreen>();
        t.FadeOut(() =>
        {
            UIManager.Instance.PopScreen();
        });
    }

    public static void BackToIntroScreen()
    {
        TransitionScreen t = UIManager.Instance.PopAllAndSwitchToScreen<TransitionScreen>();
        t.FadeIn(() =>
        {
            SceneManager.LoadScene("IntroScene");
            //IntroScreen intro = UIManager.Instance.PushScreenUnderTop<IntroScreen>();

            AudioSystem.Instance.TransitionBGMQuick(audioSetup.IntroClip);
            t.FadeOut(
                () =>
                {
                    //intro.gameObject.SetActive(true);
                    UIManager.Instance.PopScreen();
                });
        });
    }
    public static void StartLevel1()
    {
        TransitionScreen t = UIManager.Instance.PopAllAndSwitchToScreen<TransitionScreen>();
        t.FadeIn(() =>
        {
            AudioSystem.Instance.TransitionBGMQuick(audioSetup.Lv1Clip);
            SceneManager.LoadScene("LevelSelection");
            t.FadeOut(() => UIManager.Instance.PopAllAndSwitchToScreen<HudScreen>());
        });
    }


    public static void SwitchGameplayScene(string sceneName)
    {
        TransitionScreen t = UIManager.Instance.PushScreen<TransitionScreen>();
        t.FadeIn(() =>
        {
            SceneManager.LoadScene(sceneName);
            GameObject playerSpawnPos = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");
            if (playerSpawnPos)
            {
                PlayerController player = GameManager.Instance.Player;
                player.GetComponent<Rigidbody2D>().Sleep();
                GameManager.Instance.Player.transform.position = playerSpawnPos.transform.position;
                player.GetComponent<Rigidbody2D>().WakeUp();

            }
            t.FadeOut(() => UIManager.Instance.PopScreen());
        });
    }
    public static void GameEnd(bool successful)
    {
        if (successful)
        {
            GameEndSuccessful();
        }
        else
        {
            GameFailed();
        }
    }
    private static void GameEndSuccessful()
    {
        TransitionScreen t = UIManager.Instance.PopAllAndSwitchToScreen<TransitionScreen>();
        t.SetTransitionMessage("Bye Bye Grandma!");
        t.FadeIn(() =>
        {
            GameManager.Instance.StartCoroutine(WaitWithCB(2f, () =>
            {
                GameObject.Destroy(GameManager.Instance.Player.gameObject);
                SceneManager.LoadScene("IntroScene");
                AudioSystem.Instance.TransitionBGMQuick(audioSetup.IntroClip);
                t.FadeOut(() => UIManager.Instance.PopAllScreens());
            }));
        }, true);

    }
    private static void GameFailed()
    {
        TransitionScreen t = UIManager.Instance.PopAllAndSwitchToScreen<TransitionScreen>();
        t.SetTransitionMessage("You Dead With Grandma");
        t.FadeIn(() =>
        {
            GameManager.Instance.StartCoroutine(WaitWithCB(2f, () =>
            {
                GameObject.Destroy(GameCore.GameManager.Instance.Player.gameObject);
                SceneManager.LoadScene("IntroScene");
                AudioSystem.Instance.TransitionBGMQuick(audioSetup.IntroClip);
                t.FadeOut(() => UIManager.Instance.PopAllScreens());
            }));

        }, true);
    }
    static IEnumerator WaitWithCB(float duration, System.Action cb)
    {
        yield return new WaitForSeconds(duration);
        cb?.Invoke();
    }
}

