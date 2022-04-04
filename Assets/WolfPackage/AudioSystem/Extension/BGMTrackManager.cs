using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrackManager : MonoBehaviour
{
    [SerializeField] AudioSource track1;
    [SerializeField] AudioSource track2;
    AudioSource current;

    public AudioSource sfxTrack;
    private void Awake()
    {
        current = track1;
    }
    public AudioSource GetCurrentTrack()
    {
        return current;
    }
    public AudioSource NextTrack()
    {
        if(current == track1)
        {
            current = track2;
        }
        else
        {
            current = track1;
        }
        return current;
    }
}
