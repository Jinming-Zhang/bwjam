using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using GameCore;
namespace WolfUISystem.Presets
{
	public class IntroScreen : ScreenBase
	{
		public override void Initialize()
		{
		}
		public void OnStartButtonPressed()
		{

			if (GameManager.Instance)
			{
				GameManager.Instance.StartGame();
			}
			else
			{
				Debug.LogWarning("IntroScreen: Cannot find GameManager Instance");
			}
		}
		public void OnManualButtonPressed()
		{
			if (UIManager.Instance)
			{
				UIManager.Instance.PushScreen<SettingScreen>();
			}
			else
			{
				Debug.LogWarning("IntroScreen: Cannot find UIManager Instance");
			}
		}
		public void OnQuitButtonPressed()
		{
			Application.Quit();
		}
	}
}