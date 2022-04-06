using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfUISystem;
using WolfUISystem.Presets;
using DialogueSystem;
using UnityEngine.InputSystem;

public class DebugCanvas : MonoBehaviour
{
	private static DebugCanvas instance;
	public static DebugCanvas Instance => instance;
	[SerializeField]
	DialogueConversation testConversation;
	private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
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
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		DialogueScreen d = UIManager.Instance.PushScreen<DialogueScreen>();
		d.InitiateConversation(testConversation, () =>
		{
			UIManager.Instance.PopScreen();
		},
		p.GetComponent<PlayerInput>());

	}
}
