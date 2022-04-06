using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WolfAudioSystem;
using UnityEngine.UI;
public class SoundedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
	[Header("Sprite Changes")]
	[SerializeField] bool enableSpriteSwap;
	[SerializeField] Image buttonImage;
	[SerializeField] Sprite normalSprite;
	[SerializeField] Sprite hoveringSprite;
	[SerializeField] Sprite clickedSprite;

	[Header("Click Animation")]
	[SerializeField][Range(0f, 1f)] float shrinkScale = 0.9f;
	[SerializeField][Range(0f, 1f)] float shrinkDuration = 0.15f;
	float ShrinkDelta => (1 - shrinkScale) / shrinkDuration;


	[Header("Button SFX")]
	[SerializeField] AudioSource audioSource;
	[SerializeField] bool playSfxOnEnter;
	[SerializeField] bool playSfxOnClick;
	[SerializeField] AudioClip sfxEnterClip;
	[SerializeField] AudioClip sfxClickClip;

	Coroutine animationCR;
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (playSfxOnEnter && AudioSystem.Instance && sfxEnterClip)
		{
			AudioSystem.Instance.PlayUISounds(sfxEnterClip);
		}

		if (enableSpriteSwap && hoveringSprite)
		{
			buttonImage.sprite = hoveringSprite;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (enableSpriteSwap && normalSprite)
		{
			buttonImage.sprite = normalSprite;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (playSfxOnClick && AudioSystem.Instance && sfxClickClip)
		{
			AudioSystem.Instance.PlayUISounds(sfxClickClip);
		}

		if (enableSpriteSwap && clickedSprite)
		{
			buttonImage.sprite = clickedSprite;
		}
	}

	private void OnDisable()
	{
		if (enableSpriteSwap && normalSprite)
		{
			buttonImage.sprite = normalSprite;
		}
	}
	private void OnEnable()
	{
		GetComponent<RectTransform>().localScale = Vector3.one;
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		if (animationCR != null)
		{
			StopCoroutine(animationCR);
		}
		animationCR = StartCoroutine(ShrinkCR());
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (animationCR != null)
		{
			StopCoroutine(animationCR);
		}
		animationCR = StartCoroutine(ExpandCR());
	}

	IEnumerator ShrinkCR()
	{
		RectTransform rectTransform = GetComponent<RectTransform>();
		while (rectTransform.localScale.x > shrinkScale)
		{
			Vector3 newScale = rectTransform.localScale;
			newScale.x = Mathf.Max(shrinkScale, newScale.x - ShrinkDelta * Time.deltaTime);
			newScale.y = newScale.x;
			rectTransform.localScale = newScale;
			yield return new WaitForEndOfFrame();
		}
		animationCR = null;
	}

	IEnumerator ExpandCR()
	{
		RectTransform rectTransform = GetComponent<RectTransform>();
		while (rectTransform.localScale.x < 1)
		{
			Vector3 newScale = rectTransform.localScale;
			newScale.x = Mathf.Min(1, newScale.x + ShrinkDelta * Time.deltaTime);
			newScale.y = newScale.x;
			rectTransform.localScale = newScale;
			yield return new WaitForEndOfFrame();
		}
		animationCR = null;
	}

}
