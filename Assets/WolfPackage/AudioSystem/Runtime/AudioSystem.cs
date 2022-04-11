using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfAudioSystem
{
    public class AudioSystem : MonoBehaviour
    {
        private static AudioSystem instance;
        public static AudioSystem Instance => instance;
        [SerializeField] AudioConfig config;
        public AudioConfig Config => config;
        Coroutine transitionCR;

        AudioSource currentBGMAudioSource;

        // dependency to be removed
        // this is because we need to keep track of the volume of the incoming audio source if we 
        // gonna modify it based on audio config volume
        // so that it has correct volume when played multiple times
        float OverAllVolume => config.overAllVolume;
        float BGMVolume(float vol = 1) => Mathf.SmoothStep(0, 1, config.overAllVolume * config.musicVolume * vol);
        float SFXVolume(float vol = 1) => Mathf.SmoothStep(0, 1, config.overAllVolume * config.sfxVolume * vol) * 1.6f;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public AudioSystem SetOverallVolume(float val)
        {
            config.overAllVolume = val;
            if (currentBGMAudioSource)
            {
                currentBGMAudioSource.volume = BGMVolume();
            }
            return this;
        }

        public AudioSystem SetBGMVolume(float val)
        {
            config.musicVolume = val;
            if (currentBGMAudioSource)
            {
                currentBGMAudioSource.volume = BGMVolume();
            }
            return this;
        }

        public AudioSystem SetSFX(float val)
        {
            config.sfxVolume = val;
            return this;
        }

        public void PlaySFXOnAudioSource(AudioClip clip, AudioSource source, float initialVolume)
        {
            source.clip = clip;
            source.volume = SFXVolume(initialVolume);
            source.Play();
        }


        public void PlaySFXOnManagedAudioSource(AudioClip clip, IManagedAudioSource source)
        {
            source.AudioSource.clip = clip;
            source.AudioSource.volume = SFXVolume(source.InitialVolume);
            source.AudioSource.Play();
        }

        public void PlaySFXOnCamera(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, SFXVolume(1));
        }
        public void PlaySFXAtWorldPoint(AudioClip clip, Vector3 position, float volume)
        {
            AudioSource.PlayClipAtPoint(clip, position, SFXVolume(volume));
        }

        public void TransitionBGM(AudioSource src, AudioSource dst, AudioClip clip, float duration = -1, System.Action OnTransitionFinished = null, bool force = false, bool loop = true)
        {
            currentBGMAudioSource = dst;
            if (force && transitionCR != null)
            {
                StopCoroutine(transitionCR);
            }

            dst.clip = clip;
            dst.loop = loop;
            dst.Play();
            dst.volume = 0;
            float srcTotal = src.volume;
            float dstVolume = BGMVolume(1);
            transitionCR = StartCoroutine(TransitionMusckCR());

            IEnumerator TransitionMusckCR()
            {
                float realDuration = duration > 0 ? duration : config.TransitionDuration;
                while (dst.volume < dstVolume)
                {
                    float srcDelta = srcTotal / realDuration * Time.deltaTime;
                    float dstDelta = dstVolume / realDuration * Time.deltaTime;
                    src.volume = Mathf.Clamp01(src.volume - srcDelta);
                    dst.volume = Mathf.Clamp01(dst.volume + dstDelta);
                    yield return new WaitForEndOfFrame();
                }
                src.Stop();
                OnTransitionFinished?.Invoke();
                transitionCR = null;
            }
        }
    }
}
