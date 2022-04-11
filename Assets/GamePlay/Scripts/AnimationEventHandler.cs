using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfAudioSystem;
public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField]
    AudioClip ring;
    [SerializeField]
    AudioClip door;
    public void OnIntroAnimationPhoneRing()
    {
        AudioSystem.Instance.PlaySFXOnCamera(ring);
    }
    public void OnDoorInteracted()
    {
        AudioSystem.Instance.PlaySFXOnCamera(door);
    }
    public void OnIntroAnimationEventFinished()
    {
        GameCore.GameManager.Instance.Initialize();
    }

}
