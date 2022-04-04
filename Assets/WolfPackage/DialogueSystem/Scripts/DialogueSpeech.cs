using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DialogueSystem
{
    //[CreateAssetMenu(menuName = "Dialogue/Dialogue Speech", fileName = "Dialogue Speech")]
    [System.Serializable]
    public class DialogueSpeech : DialogueComponent
    {
        public Sprite BackgroundSprite;
        public Sprite characterSprite;
        public string characterName;
        public bool keepPreviousBackground = true;

        public List<DialogueOption> options;
        public string Speech;

        public string CharacterName => characterName;

        public Sprite IconSprite => characterSprite;

        public List<DialogueOption> DialogueOptions => options;

        public string SpeechLine => Speech;

        public bool KeepPreviousElements => keepPreviousBackground;

        Sprite DialogueComponent.BackgroundSprite => BackgroundSprite;

        public bool HasOptions()
        {
            return options != null && options.Count > 0;
        }
    }
}