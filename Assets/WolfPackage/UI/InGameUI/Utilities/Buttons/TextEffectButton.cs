using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class TextEffectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	TMPro.TextMeshProUGUI buttonTextTMP;
	[SerializeField]
	string buttonText;
	// [SerializeField]
	// [Tooltip("Need to be an effect supported by TextAnimator")]
	// string textEffect = "shake";
	TextAnimator _textAnimator;
	TextAnimator textAnimator
	{
		get
		{
			if (!_textAnimator)
			{
				_textAnimator = buttonTextTMP.GetComponent<TextAnimator>();
			}
			return _textAnimator;
		}
	}
	private void Start()
	{
		if (string.IsNullOrEmpty(buttonText))
		{
			buttonText = buttonTextTMP.text;
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		// if (textAnimator)
		// {
		// 	textAnimator.enabled = true;
		// 	buttonTextTMP.text = $"<{textEffect}>{buttonText}</{textEffect}>";
		// }
		buttonTextTMP.gameObject.transform.localScale = Vector3.one * 1.1f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonTextTMP.gameObject.transform.localScale = Vector3.one;
		// if (textAnimator)

		// {
		// 	textAnimator.enabled = false;
		// }
		// buttonTextTMP.SetText(buttonText);
	}
}