using UnityEngine;

public interface IManagedAudioSource
{
	float InitialVolume { get; }
	AudioSource AudioSource { get; }
}
