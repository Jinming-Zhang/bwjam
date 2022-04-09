using UnityEngine;
using UnityEngine.InputSystem;
using WolfUISystem;
using WolfUISystem.Presets;
public class NPC : MonoBehaviour, IIteractable
{
    [SerializeField]
    DialogueSystem.DialogueConversation conversation;
    [SerializeField]
    float interactionRange = 1f;
    [SerializeField]
    GameObject icon;
    private void Start()
    {
        icon.SetActive(false);
    }
    public void Interact(GameObject initiator)
    {
        if (conversation)
        {
            UIManager.Instance.PushScreen<DialogueScreen>().InitiateConversation(conversation, () => UIManager.Instance.PopScreen(), initiator.GetComponent<PlayerInput>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            player.InteractTarget = this;
            icon.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteractor player = collision.GetComponent<PlayerInteractor>();
        if (player)
        {
            player.InteractTarget = null;
            icon.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
