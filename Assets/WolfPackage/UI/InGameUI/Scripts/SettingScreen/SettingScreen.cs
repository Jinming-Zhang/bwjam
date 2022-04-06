using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WolfUISystem;

namespace WolfUISystem.Presets
{
	[System.Serializable]
	public class SettingsMap
	{
		public Button OptionButton;

		public GameObject OptionPanel;
	}

	public class SettingScreen : ScreenBase
	{
		[SerializeField]
		List<SettingsMap> settingScreenPanels;

		public override void Initialize()
		{
			if (settingScreenPanels != null)
			{
				foreach (SettingsMap map in settingScreenPanels)
				{
					map.OptionButton.onClick.RemoveAllListeners();
					map
						.OptionButton
						.onClick
						.AddListener(() =>
						{
							SwitchToPanel(map);
						});
				}
			}
		}

		public void SwitchToPanel(SettingsMap buttonPanelMap)
		{
			foreach (SettingsMap map in settingScreenPanels)
			{
				map
					.OptionButton
					.GetComponent<ButtonToggleSpriteSwapper>()
					.IsSelected = map == buttonPanelMap;
				map.OptionPanel.SetActive(map == buttonPanelMap);
			}
		}
		public void Hide()
		{
			UIManager.Instance.PopScreen();
		}
		public override void OnScreenPoppedOut()
		{
			base.OnScreenPoppedOut();
			GameCore.GameManager.Instance.ResumeGame();
		}
		public override void OnScreenPushedIn()
		{
			base.OnScreenPushedIn();
			GameCore.GameManager.Instance.PauseGame();
		}
		private void OnEnable()
		{
			if (settingScreenPanels.Count > 0)
			{
				SwitchToPanel(settingScreenPanels[0]);
			}
		}
	}
}
