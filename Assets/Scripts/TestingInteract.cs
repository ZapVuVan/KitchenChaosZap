using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInteract : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += PlayerInput_onActionTriggered;


    }
    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
    }
}
