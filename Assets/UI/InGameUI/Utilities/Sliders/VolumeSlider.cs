using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WolfAudioSystem;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider overallSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    bool addedListners = false;
    public void Initialize()
    {
        if(AudioSystem.Instance)
        {
            AudioConfig config = AudioSystem.Instance.Config;
            overallSlider.value = config.overAllVolume;
            bgmSlider.value = config.musicVolume;
            sfxSlider.value = config.sfxVolume;

            if(!addedListners)
            {
                overallSlider.onValueChanged.AddListener((val) =>
                {
                    AudioSystem.Instance.SetOverallVolume(val);
                });

                bgmSlider.onValueChanged.AddListener((val) =>
                {
                    AudioSystem.Instance.SetBGMVolume(val);
                });

                sfxSlider.onValueChanged.AddListener((val) =>
                {
                    AudioSystem.Instance.SetSFX(val);
                });
                addedListners = true;
            }
        }
        else
        {
            Debug.LogWarning($"{this.GetType()}: Failed to setup volume sliders, AudioSystem not initialized.");
        }
    }
}
