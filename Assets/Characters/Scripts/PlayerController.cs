using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = moveSpeed * movement * Time.deltaTime;
        transform.position =
            transform.position + new Vector3(delta.x, delta.y, 0);
    }

    public void OnPlayerMoved(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            movement = ctx.ReadValue<Vector2>();
        }
        else if (ctx.canceled)
        {
            movement = Vector2.zero;
        }
    }
}
