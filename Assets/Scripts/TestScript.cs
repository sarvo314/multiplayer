using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    private Rigidbody sphereRigidBody;
    private PlayerInput playerInput;

    private void Awake()
    {
        sphereRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log("Hey there!");
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sphereRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        //Debug.Log("Jump is pressed");
    }

}
