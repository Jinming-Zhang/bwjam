using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;

namespace WolfUISystem.Presets
{
	public class HudScreen : ScreenBase
	{
		public override void Initialize()
		{
		}
		public void OnHelpButtonClicked()
		{
			UIManager.Instance.PushScreen<SettingScreen>();
		}
	}
}