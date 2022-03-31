using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using GameUI;

public class DebugCanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TranInOut(bool hasText)
    {
        TransitionScreen t = UIManager.Instance.PushScreen<TransitionScreen>();
        t.SetTransitionMessage("random fjfjfjfjeieasdfasdfa");
        t.TransitionTime = 3f;
        t.FadeIn(() =>
        {
            StartCoroutine(WaitSeconds(2,
                () =>
                {
                    t.FadeOut(() => UIManager.Instance.PopScreen(), hasText);
                }
                ));
        }, hasText);
    }
    IEnumerator WaitSeconds(float s, System.Action cb = null)
    {
        yield return new WaitForSeconds(s);
        cb?.Invoke();
    }
    public void RandoMTesting()
    {
        UIManager.Instance.PopAllAndSwitchToScreen<SettingScreen>();
    }
}
