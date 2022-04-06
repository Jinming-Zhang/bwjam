using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfAudioSystem;

namespace GameCore
{
	public class GameResourceLocator : MonoBehaviour
	{
		private static GameResourceLocator instance;

		public static GameResourceLocator Instance => instance;

		public BGMTrackManager bgmTrackManager;

		public GameAudioSetup audioSetup;
		public MainCamCtrl mainCamControl;


		private void Awake()
		{
			if (instance && instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
			}
		}
	}
}
