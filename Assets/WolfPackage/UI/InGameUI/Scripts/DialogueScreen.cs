using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WolfUISystem;

namespace WolfUISystem.Presets
{
	public class DialogueScreen : ScreenBase
	{
		[Header("Input Actions Config")]
		[SerializeField]
		string DialogueActionMapName = "Dialogue";
		[SerializeField]
		string continueActionName = "Continue";
		string previousActionMapName;

		[Header("Dialogue Panel")]
		[SerializeField]
		Image background;

		[SerializeField]
		Image iconSprite;

		[SerializeField]
		TMPro.TextMeshProUGUI characterName;

		[SerializeField]
		TMPro.TextMeshProUGUI speech;

		[SerializeField]
		GameObject optionRoot;

		[SerializeField]
		DialogueOptionButton optionButton;

		Action onConversationFinished;

		DialogueConversation currentConversation;
		// Text Revealing
		[Header("Revealing Text Config")]
		[SerializeField]
		[Range(0, 40)]
		int charPerSec = 20;
		Coroutine revealTextCR;
		string revealingText;
		public override void Initialize()
		{
		}

		public void InitiateConversation(
			DialogueConversation conversation,
			Action finishedCB,
			PlayerInput playerInput = null
		)
		{
			if (currentConversation)
			{
				Debug
					.LogError($"{GetType()}: Cannot Initiate new conversation before current one finishes");
				//return;
			}
			RegisterPlayerInput(finishedCB, playerInput);

			currentConversation = conversation;
			conversation.Reset();

			gameObject.SetActive(true);
			UpdateDialoguePanel();

			void RegisterPlayerInput(Action finishedCB, PlayerInput playerInput)
			{
				if (playerInput)
				{
					previousActionMapName = playerInput.currentActionMap.name;
					playerInput.SwitchCurrentActionMap(DialogueActionMapName);
					playerInput.actions[continueActionName].performed += OnNextPressed;

					onConversationFinished = () =>
					{
						finishedCB?.Invoke();
						playerInput.actions[continueActionName].performed -= OnNextPressed;
						playerInput.SwitchCurrentActionMap(previousActionMapName);
					};
				}
			}
		}

		public void UpdateDialoguePanel()
		{
			foreach (Transform item in optionRoot.transform)
			{
				Destroy(item.gameObject);
			}

			if (currentConversation.MoveNext())
			{
				DialogueComponent component = currentConversation.Current;
				Background(component);
				CharacterName(component);
				CharacterIcon(component);
				Speech(component);
				Options(component);
			}
			else
			{
				currentConversation.Reset();
				onConversationFinished?.Invoke();
				currentConversation = null;
			}

			void Background(DialogueComponent component)
			{
				if (component.BackgroundSprite)
				{
					background.gameObject.SetActive(true);
					background.sprite = component.BackgroundSprite;
				}
				else
				{
					if (!component.KeepPreviousElements)
					{
						background.gameObject.SetActive(false);
					}
				}
			}

			void CharacterName(DialogueComponent component)
			{
				if (!string.IsNullOrEmpty(component.CharacterName))
				{
					characterName.text = component.CharacterName;
				}
				else
				{
					if (!component.KeepPreviousElements)
					{
						characterName.text = String.Empty;
					}
				}
			}

			void CharacterIcon(DialogueComponent component)
			{
				if (component.IconSprite)
				{
					iconSprite.color = Color.white;
					iconSprite.sprite = component.IconSprite;
				}
				else
				{
					if (!component.KeepPreviousElements)
					{
						iconSprite.gameObject.SetActive(false);
						Color w = Color.white;
						w.a = 0;
						iconSprite.color = w;
					}
				}
			}

			void Speech(DialogueComponent component)
			{
				if (!string.IsNullOrEmpty(component.SpeechLine))
				{
					//revealTextCR = StartCoroutine(RevealTextCR());
					speech.text = component.SpeechLine;
                }
				else
				{
					speech.text = string.Empty;
				}
				IEnumerator RevealTextCR()
				{
					speech.text = component.SpeechLine;
					revealingText = speech.text;
					speech.maxVisibleCharacters = 0;
					while (speech.maxVisibleCharacters < speech.text.Length)
					{
						speech.maxVisibleCharacters++;
						yield return new WaitForSeconds(1f / charPerSec);
					}
					revealTextCR = null;
					revealingText = string.Empty;
				}
			}

			void Options(DialogueComponent component)
			{
				if (component.HasOptions())
				{
					//playerInput.actions.FindAction("Next").Disable();
					foreach (DialogueOption option in component.DialogueOptions)
					{
						DialogueOptionButton o =
							Instantiate(optionButton, optionRoot.transform);
						o.Initialize(option, (o) => OnOptionButtonClicked(o));
					}
				}
			}
		}

		void OnOptionButtonClicked(DialogueOption o)
		{
			currentConversation.UpdateOption(o);
		}

		public void OnNextPressed(InputAction.CallbackContext ctx)
		{
			if (ctx.performed)
			{
				if (revealTextCR != null)
				{
					StopCoroutine(revealTextCR);
					revealTextCR = null;
					speech.maxVisibleCharacters = revealingText.Length;
					revealingText = string.Empty;
				}
				else
				{
					UpdateDialoguePanel();
				}
			}
		}
	}
}
