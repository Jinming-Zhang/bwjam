using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem.Presets;

public static class IngameUIOperationHandler
{
	public static List<ButtonGroupOption> ConstructIngameButtonPopup()
	{
		List<ButtonGroupOption> arg = new List<ButtonGroupOption>();

		// restart option
		ButtonGroupOption restartOption = new ButtonGroupOption();
		restartOption.ButtonText = "Give up";
		restartOption.OnButtonClickedHandler = () => { GameCore.GameManager.Instance.BackToIntroScreen(); };
		// manual option
		ButtonGroupOption manualBtnOpt = new ButtonGroupOption();
		manualBtnOpt.ButtonText = "Manual";
		manualBtnOpt.OnButtonClickedHandler = () => { WolfUISystem.UIManager.Instance.PushScreen<SettingScreen>(); };
		// quit option
		ButtonGroupOption quitOption = new ButtonGroupOption();
		quitOption.ButtonText = "Quit Game";
		quitOption.OnButtonClickedHandler = () => Application.Quit();

		arg.Add(restartOption);
		arg.Add(manualBtnOpt);
		arg.Add(quitOption);
		return arg;
	}
}