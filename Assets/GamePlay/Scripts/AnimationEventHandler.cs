using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfAudioSystem;
public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField]
    AudioClip ring;
    public void OnIntroAnimationPhoneRing()
    {
        AudioSystem.Instance.PlaySFXOnCamera(ring);
    }
    public void OnIntroAnimationEventFinished()
    {
        GameCore.GameManager.Instance.Initialize();
    }

}
