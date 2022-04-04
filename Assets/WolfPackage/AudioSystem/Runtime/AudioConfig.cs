using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wolfuroll/Audio System/Audio Config", fileName = "Audio Config")]
public class AudioConfig : ScriptableObject
{
    [Range(0f, 1f)]
    public float overAllVolume;
    [Range(0f, 1f)]
    public float musicVolume;
    [Range(0f, 1f)]
    public float sfxVolume;

    public float TransitionDuration;
}
