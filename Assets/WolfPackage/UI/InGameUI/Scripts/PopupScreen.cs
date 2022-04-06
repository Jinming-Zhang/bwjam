using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
namespace WolfUISystem.Presets
{
	public class PopupScreen : ScreenBase
	{
		public enum PopupType
		{
			ButtonGroup
		}

		[SerializeField]
		List<PopupScreenPanel> popups;

		public void InitializePopup(PopupType popupType, object arg)
		{
			foreach (PopupScreenPanel panel in popups)
			{
				if (panel.popupType == popupType)
				{
					panel.Initialize(arg);
				}
				else
				{
					panel.Hide();
				}
			}
		}

		public override void Initialize()
		{
			// foreach (PopupScreenPanel panel in popups)
			// {
			// 	panel.Hide();
			// }
		}

		public override void OnScreenPushedIn()
		{
			base.OnScreenPushedIn();
			GameCore.GameManager.Instance.PauseGame();
		}

		public override void OnScreenPoppedOut()
		{
			base.OnScreenPoppedOut();
			GameCore.GameManager.Instance.ResumeGame();
		}

		public void OnGiveUPClicked()
		{
			GameCore.GameManager.Instance.BackToIntroScreen();
		}
		public void OnManualClicked()
		{
			UIManager.Instance.PushScreen<SettingScreen>();
		}
		public void OnQuitClicked()
		{
			Application.Quit();
		}
	}
}
