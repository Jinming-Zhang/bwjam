using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace DialogueSystem
{
    public interface DialogueComponent
    {
        Sprite BackgroundSprite
        {
            get;
        }

        bool KeepPreviousElements
        {
            get;
        }

        string CharacterName
        {
            get;
        }
        Sprite IconSprite
        {
            get;
        }
        bool HasOptions();
        List<DialogueOption> DialogueOptions
        {
            get;
        }
        string SpeechLine
        {
            get;
        }
    }
}