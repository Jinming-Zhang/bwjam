using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerInteractor : MonoBehaviour
{
    IIteractable interactTarget;
    public IIteractable InteractTarget { get => interactTarget; set => interactTarget = value; }
    public void OnInteractPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && InteractTarget != null)
        {
            InteractTarget.Interact(gameObject);
        }
    }
}
