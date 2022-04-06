using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WolfAudioSystem
{
	[CreateAssetMenu(fileName = "Game Audio Setup")]
	public class GameAudioSetup : ScriptableObject
	{
		public AudioClip IntroClip;
		public AudioClip Lv1Clip;
	}
}