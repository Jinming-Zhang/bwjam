using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Option", fileName = "Dialogue Option")]
    public class DialogueOption : ScriptableObject
    {
        public string optionText;
        public List<DialogueSpeech> optionSpeeches;
    }
}