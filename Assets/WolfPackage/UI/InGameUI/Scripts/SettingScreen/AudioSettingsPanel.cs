using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsPanel : MonoBehaviour
{
    [SerializeField] VolumeSlider volumeSlider;
    private void OnEnable()
    {
        volumeSlider.Initialize();
    }
}
