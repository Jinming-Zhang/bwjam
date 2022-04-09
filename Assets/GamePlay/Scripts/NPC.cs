using UnityEngine;
using UnityEngine.InputSystem;
using WolfUISystem;
using WolfUISystem.Presets;
public class NPC : InteractibleObject
{
    [SerializeField]
    DialogueSystem.DialogueConversation conversation;

    public override void Interact(GameObject initiator)
    {
        if (conversation)
        {
            UIManager.Instance.PushScreen<DialogueScreen>().InitiateConversation(conversation, () => UIManager.Instance.PopScreen(), initiator.GetComponent<PlayerInput>());
        }
    }
}
