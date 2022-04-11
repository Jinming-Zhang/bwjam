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
		public AudioClip EnemyKill1;
		public AudioClip EnemyKill2;
		public AudioClip EnemyKill3;
		public AudioClip EnemyKill4;
		public AudioClip BossFight;
		// sfx for player
		public AudioClip PlayerDead;
		public AudioClip PlayerHit;
		// sfx for boss
		public AudioClip BossKiss;
		// sfx for spirit enemy
		public AudioClip SpiritStab;
	}
}