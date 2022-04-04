using System.Collections;
using UnityEngine;
using WolfUISystem;
using System;
using UnityEngine.UI;

public class TransitionScreen : ScreenBase
{
    [SerializeField] Image transitionImage;
    // transition infos
    [SerializeField] CanvasGroup transitionInfoCg;
    [SerializeField] TMPro.TextMeshProUGUI transitionMsg;

    private float transitionDuration = 1f;
    public float TransitionTime
    {
        get => transitionDuration;
        set => transitionDuration = value;
    }

    public override void Initialize()
    {
    }

    public void SetTransitionMessage(string msg)
    {
        transitionMsg.text = msg;
        transitionMsg.gameObject.SetActive(true);
    }

    public void SetTransitionSprite(Sprite s)
    {
        transitionImage.sprite = s;
    }

    public void FadeIn(Action finishedCB = null, bool showInfo = false)
    {
        transitionImage.type = Image.Type.Sliced;
        StartCoroutine(FadeInCR());
        IEnumerator FadeInCR()
        {
            Color tmpClr = transitionImage.color;
            tmpClr.a = 0;
            transitionImage.color = tmpClr;

            transitionInfoCg.gameObject.SetActive(showInfo);
            transitionInfoCg.alpha = 0;

            float delta = 1f / transitionDuration * Time.deltaTime;
            float linearA = 0;
            while (linearA < 1)
            {
                linearA = Mathf.Min(1f, linearA + delta);
                float targetA = Mathf.SmoothStep(0, 1, linearA);
                // transition image
                tmpClr = transitionImage.color;
                tmpClr.a = targetA;
                transitionImage.color = tmpClr;

                // transition info
                if (showInfo)
                {
                    transitionInfoCg.alpha = targetA;
                }

                yield return new WaitForEndOfFrame();
            }
            finishedCB?.Invoke();
        }
    }

    public void FadeOut(Action finishedCB = null, bool showInfo = false)
    {
        transitionImage.type = Image.Type.Sliced;
        StartCoroutine(FadeInOut());
        IEnumerator FadeInOut()
        {
            // tarnsition image
            Color tmpClr = transitionImage.color;
            tmpClr.a = 1;
            transitionImage.color = tmpClr;

            // transition info
            transitionInfoCg.gameObject.SetActive(showInfo);

            float delta = 1f / transitionDuration * Time.deltaTime;
            float linearA = 1;
            while (linearA > 0)
            {
                linearA = Mathf.Max(0f, linearA - delta);
                float targetA = Mathf.SmoothStep(0, 1, linearA);
                // transition image
                tmpClr = transitionImage.color;
                tmpClr.a = targetA;
                transitionImage.color = tmpClr;

                // transition info
                if (showInfo)
                {
                    transitionInfoCg.alpha = targetA;
                }

                yield return new WaitForEndOfFrame();
            }
            finishedCB?.Invoke();
        }
    }

    public void SpinIn(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Radial360;

    }

    public void SpinOut(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Radial360;

    }

    public void HorizontalSlideIn(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Horizontal;

    }

    public void HorizontalSlideOut(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Horizontal;

    }

    public void VerticalSlideIn(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Vertical;

    }
    public void VerticalSlideOut(Action finished = null)
    {
        transitionImage.type = Image.Type.Filled;
        transitionImage.fillMethod = Image.FillMethod.Vertical;

    }
    public void ShowSpinnerOverlay()
    {
    }

    public void HideSpinnerOverlay()
    {

    }
}

