using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using WolfAudioSystem;
using WolfUISystem.Presets;
using GameCore;
using UnityEngine.SceneManagement;
public static class GameSequence
{
	static GameAudioSetup audioSetup => GameManager.Instance.ResourceLocator.audioSetup;
	public static void TransitionToIntroScene()
	{
		AudioSystem.Instance.TransitionBGMQuick(audioSetup.IntroClip);

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
			IntroScreen intro = UIManager.Instance.PushScreenUnderTop<IntroScreen>();

			AudioSystem.Instance.TransitionBGMQuick(audioSetup.IntroClip);
			t.FadeOut(
				() =>
				{
					intro.gameObject.SetActive(true);
					UIManager.Instance.PopScreen();
				});
		});
	}
	public static void StartLevel1()
	{
		TransitionScreen t = UIManager.Instance.PopAllAndSwitchToScreen<TransitionScreen>();
		t.FadeIn(() =>
		{
			SceneManager.LoadScene("TestLevel");
			AudioSystem.Instance.TransitionBGMQuick(audioSetup.Lv1Clip);
			t.FadeOut(() => UIManager.Instance.PopAllAndSwitchToScreen<HudScreen>());
		});
	}
}

