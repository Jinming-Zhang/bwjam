using System.Collections;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
using WolfAudioSystem;

public static class AudioSystemExt
{
	public static void TransitionBGMQuick(this AudioSystem audiosystem, AudioClip newClip)
	{
		AudioSource current = GameManager.Instance.ResourceLocator.bgmTrackManager.GetCurrentTrack();
		AudioSource next = GameManager.Instance.ResourceLocator.bgmTrackManager.NextTrack();
		AudioSystem.Instance.TransitionBGM(current, next, newClip);
	}

	public static void PlayUISounds(this AudioSystem audiosystem, AudioClip newClip, float volume = 1)
	{
		audiosystem.PlaySFXOnAudioSource(newClip, GameManager.Instance.ResourceLocator.bgmTrackManager.sfxTrack, volume);
	}
}
