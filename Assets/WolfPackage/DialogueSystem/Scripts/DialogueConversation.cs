using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Conversation", fileName = "Dialogue Conversation")]
    public class DialogueConversation : ScriptableObject, IEnumerator<DialogueSpeech>
    {
        public List<DialogueSpeech> DialogueComponents;
        private List<DialogueSpeech> dynamicDialogueComponents;
        int dynamicIndex = -1;

        public DialogueSpeech Current => dynamicDialogueComponents[dynamicIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            dynamicIndex++;
            return dynamicIndex < dynamicDialogueComponents.Count;
        }

        public void Reset()
        {
            dynamicIndex = -1;
            dynamicDialogueComponents = DialogueComponents;
        }

        public void UpdateOption(DialogueOption selectedOption)
        {
            DialogueOption option = Current.DialogueOptions.Find(o => o == selectedOption);
            dynamicDialogueComponents.InsertRange(dynamicIndex + 1, option.optionSpeeches);
        }
    }
}